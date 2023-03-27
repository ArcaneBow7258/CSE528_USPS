using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;
using Unity.Services.Authentication;
using Unity.Services.Lobbies;
using Unity.Services.Core;
using Unity.Services.Lobbies.Models;
using TMPro;
using UnityEngine.UI;
public class LobbyManager : MonoBehaviour
{  
    //
    
    //lobby data
    public Lobby currentLobby;
    private Player loggedInPlayer;
    private float pollTimer;
    public static LobbyManager Instance;

    
    //Event triggers
    public UnityEvent e_lobbyUpdate;
    public UnityEvent e_swapLobby;

    void Awake(){
        if(Instance != null){
            Debug.Log("You have two lobby managers");
        }
        else{
            Instance = this;
        }
        if(e_lobbyUpdate==null) e_lobbyUpdate = new UnityEvent();
        if(e_swapLobby== null) e_swapLobby= new UnityEvent();
    }
    // Start is called before the first frame update
    async void Start()
    {
        //log into service, need to run before doing API calls
        await UnityServices.InitializeAsync();
        loggedInPlayer = await GetPlayerFromAnonymousLoginAsync();
        //AuthenticationService.Instance.IsSignedIn;
        //e_lobbyUpdate.AddListener(updatePanels);

    }
    
    public async void StartGame(){
        if(IsLobbyHost()){
            try{
                Debug.Log("Start Game");
                string relayCode = await RelayManager.Instance.CreateRelay(currentLobby.MaxPlayers);

                Lobby lobby = await Lobbies.Instance.UpdateLobbyAsync(currentLobby.Id, new UpdateLobbyOptions{
                    Data = new Dictionary<string, DataObject> {
                        { "RelayCode", new DataObject(DataObject.VisibilityOptions.Member, relayCode)}
                    }
                });
                currentLobby = lobby;

            }
            catch(Exception e){
                Debug.Log("Error starting game: "+ e);
            }
        }
    }

    public bool IsLobbyHost(){
        return currentLobby.HostId == AuthenticationService.Instance.PlayerId;
    }
    
    //ready up?
    //loggedInPlayer.Data.Add("Ready", new PlayerDataObject(PlayerDataObject.VisibilityOptions.Member, "No"));
    public async Task createLobby(string n,int size, string diff, string len, bool priv){
        if(currentLobby == null){
            try{
                
                
                var lobbyData = new Dictionary<string, DataObject>()
                    {
                        //indexed string data
                        ["LobbyName"] = new DataObject(DataObject.VisibilityOptions.Public, n, DataObject.IndexOptions.S1), //depreciated?
                        ["RelayCode"] = new DataObject(DataObject.VisibilityOptions.Member, "0"),
                        ["Difficulty"] = new DataObject(DataObject.VisibilityOptions.Public, diff, DataObject.IndexOptions.N1),
                        ["Length"] = new DataObject(DataObject.VisibilityOptions.Public, len,DataObject.IndexOptions.N2),
                    };

                    Lobby lobby = await LobbyService.Instance.CreateLobbyAsync(
                        lobbyName: n,//+Guid.NewGuid(),
                        maxPlayers: size,
                        options: new CreateLobbyOptions()
                        {
                            Data = lobbyData,
                            IsPrivate = priv,
                            Player = loggedInPlayer
                        });
                    currentLobby = lobby;
                    e_swapLobby.Invoke();
                Debug.Log($"Created new lobby {currentLobby.Name} ({currentLobby.Id})");
                StartCoroutine(Heartbeat(currentLobby.Id,15));
            }catch(LobbyServiceException e){
                Debug.Log(e);
            }
        }else{
            Debug.Log("Currently in a lobby");
        }
             
    }
    IEnumerator<WaitForSecondsRealtime> Heartbeat(string lobbyId, float wait){
        var delay = new WaitForSecondsRealtime(wait);
            
        while(true){
            LobbyService.Instance.SendHeartbeatPingAsync(lobbyId);
            yield return delay;
        }
    }
    async void DeleteLobby(){
        await LobbyService.Instance.DeleteLobbyAsync(currentLobby.Id);
    }
    void OnApplicationQuit(){
        string playerId = AuthenticationService.Instance.PlayerId;
        if(currentLobby != null && loggedInPlayer != null){
            if(currentLobby.Id == playerId){
                LobbyService.Instance.DeleteLobbyAsync(currentLobby.Id);
            }else{
                LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, playerId );
            }
            
        }
    }
    public async Task joinByQuick(){
        //straight from unity abby
        try
            {
                // Quick-join a random lobby with a maximum capacity of 10 or more players.
                QuickJoinLobbyOptions options = new QuickJoinLobbyOptions();
                //filter options to join only lobies with slots greater than 0
                options.Filter = new List<QueryFilter>()
                {
                    new QueryFilter(
                        field: QueryFilter.FieldOptions.AvailableSlots,
                        op: QueryFilter.OpOptions.GT,
                        value: "0")
                /*
                // Let's add some filters for custom indexed fields
                new QueryFilter(
                    field: QueryFilter.FieldOptions.S1, // S1 = "Test"
                    op: QueryFilter.OpOptions.EQ,
                    value: "true"),

                new QueryFilter(
                    field: QueryFilter.FieldOptions.S2, // S2 = "GameMode"
                    op: QueryFilter.OpOptions.EQ,
                    value: "ctf"),

                // Example "skill" range filter (skill is a custom numeric field in this example)
                new QueryFilter(
                    field: QueryFilter.FieldOptions.N1, // N1 = "Skill"
                    op: QueryFilter.OpOptions.GT,
                    value: "0"),

                new QueryFilter(
                    field: QueryFilter.FieldOptions.N1, // N1 = "Skill"
                    op: QueryFilter.OpOptions.LT,
                    value: "51"),*/
                };
                if(currentLobby == null){
                    Lobby lobby = await LobbyService.Instance.QuickJoinLobbyAsync(options);
                    currentLobby = lobby;
                    Debug.Log("Joined Lobby " + lobby.Name);
                    e_swapLobby.Invoke();
                    }
                else{
                    Debug.Log("already in lobby");
                }
                
            }
            catch (LobbyServiceException e)
            {
                Debug.Log(e);
            }
    }
    public async Task joinById(string lobbyId){
        try
        {
            if(currentLobby == null){
                Lobby lobby = await LobbyService.Instance.JoinLobbyByIdAsync(lobbyId);
                currentLobby = lobby;
                Debug.Log("Joined Lobby " + lobby.Name);
            }
            else{
                Debug.Log("already in lobby");
            }
            
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }

    }

    public async Task joinByCode(string lobbyCode){
        try
        {
            if(currentLobby == null){
                Lobby lobby =await LobbyService.Instance.JoinLobbyByCodeAsync("lobbyCode");
                currentLobby = lobby;
                Debug.Log("Joined Lobby " + lobby.Name);
                e_swapLobby.Invoke();
            }
            else{
                Debug.Log("already in lobby");
            }
        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
    
    //probably can modify to kick player
    async Task leaveLobby(){
        try
        {
                //Ensure you sign-in before calling Authentication Instance
                //See IAuthenticationService interface
                string playerId = AuthenticationService.Instance.PlayerId;
                await LobbyService.Instance.RemovePlayerAsync(currentLobby.Id, playerId);
                currentLobby = null;
                e_swapLobby.Invoke();
        }
        catch (LobbyServiceException e)
        {
                Debug.Log(e);
        }
    }
    //shell functions
    async Task Reconnect(){
        //await LobbyService.Instance.ReconnectToLobbyAsync(lobbyApi);
    }
    async Task updateLobby(){
        UpdateLobbyOptions options = new UpdateLobbyOptions();
        options.Name = "testLobbyName";
        options.MaxPlayers = 4;
        options.IsPrivate = false;

        //Ensure you sign-in before calling Authentication Instance
        //See IAuthenticationService interface
        options.HostId = AuthenticationService.Instance.PlayerId;

        options.Data = new Dictionary<string, DataObject>()
        {
            {
                "ExamplePrivateData", new DataObject(
                    visibility: DataObject.VisibilityOptions.Private,
                    value: "PrivateData")
            },
            {
                "ExamplePublicData", new DataObject(
                    visibility: DataObject.VisibilityOptions.Public,
                    value: "PublicData",
                    index: DataObject.IndexOptions.S1)
            },
        };
        //update host
        //urrentLobby.HostId = 
        var lobby = await LobbyService.Instance.UpdateLobbyAsync(currentLobby.Id, options);

    }
    async Task updatePlayer(){
        UpdatePlayerOptions options = new UpdatePlayerOptions();

        options.Data = new Dictionary<string, PlayerDataObject>()
        {
            {
                "existing data key", new PlayerDataObject(
                    visibility: PlayerDataObject.VisibilityOptions.Private,
                    value: "updated data value")
            },
            {
                "new data key", new PlayerDataObject(
                    visibility: PlayerDataObject.VisibilityOptions.Public,
                    value: "new data value")
            }
        };

        //Ensure you sign-in before calling Authentication Instance
        //See IAuthenticationService interface
        string playerId = AuthenticationService.Instance.PlayerId;
        await LobbyService.Instance.UpdatePlayerAsync(currentLobby.Id, playerId, options);
    }
    
    // Log in a player using Unity's "Anonymous Login" API and construct a Player object for use with the Lobbies APIs
    static async Task<Player> GetPlayerFromAnonymousLoginAsync()
    {
        if (!AuthenticationService.Instance.IsSignedIn)
        {
            Debug.Log($"Trying to log in a player ...");

            // Use Unity Authentication to log in
            await AuthenticationService.Instance.SignInAnonymouslyAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                throw new InvalidOperationException("Player was not signed in successfully; unable to continue without a logged in player");
            }
        }

        Debug.Log("Player signed in as " + AuthenticationService.Instance.PlayerId);

        // Player objects have Get-only properties, so you need to initialize the data bag here if you want to use it
        return new Player(AuthenticationService.Instance.PlayerId, null, new Dictionary<string, PlayerDataObject>());
    }

    private async void pollLobby(){
        if(currentLobby != null){
            
            pollTimer -= Time.deltaTime;
            if(pollTimer< 0f){
                pollTimer = 5.0f;
                Lobby lobby = await LobbyService.Instance.GetLobbyAsync(currentLobby.Id);
                if(currentLobby.LobbyCode != lobby.LobbyCode){
                    Debug.Log("Lobby mismatch on poll");
                    
                }else{
                    currentLobby = lobby;
                    e_lobbyUpdate.Invoke(); //recheck lobby panel
                }
                if(!IsLobbyHost()){
                    if(currentLobby.Data["RelayCode"].Value != "0"){
                        RelayManager.Instance.JoinRelay(currentLobby.Data["RelayCode"].Value);
                    }
                }
            }
        }

    }
    
    void Update(){
        pollLobby();
    }



}
