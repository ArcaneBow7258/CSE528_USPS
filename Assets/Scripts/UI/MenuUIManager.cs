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
public class MenuUIManager : MonoBehaviour
{
    /// <summary>
    ///  UI Windows
    /// </summary>
    public GameObject u_skilltree;
    public GameObject u_skillSelect;
    //menu
    public GameObject u_mainmenu;
    //entire lobby interface
    public GameObject u_lobby;
    //large lobby 
    public GameObject u_joinLobby;


    public GameObject backButton;
    
    //create lobby
    public TMP_InputField lname;
    public TMP_Dropdown size;
    public Toggle visibility;
    public TMP_Dropdown difficulty;
    public TMP_Dropdown length;
    public TMP_InputField joinCode;
    //join lobby
    public GameObject lobbyList;
    public GameObject lobbyPanel;
    //public Button refresh;

    //while in lobby
    public GameObject u_inLobby;
    //Game Lobby Ui
    public GameObject playerList;
    public GameObject playerPanel;
    public LobbyInfo gameInfo;

    //Events
    UnityEvent e_swapLobby;
    private GameObject previous = null;
    private GameObject current;
    
    

    void Awake(){
        //you shuldn't be lobby in awake right..
        u_mainmenu.SetActive(true);
        //lobby stuff
        u_lobby.SetActive(false);
        u_inLobby.SetActive(false);
        u_joinLobby.SetActive(true);
        u_skillSelect.SetActive(false);
        //build and unload
        u_skilltree.SetActive(true);
        u_skilltree.SetActive(false);
        current = u_mainmenu;


    }
    void Start(){
        LobbyManager.Instance.e_swapLobby.AddListener(swapLobby);
        LobbyManager.Instance.e_lobbyUpdate.AddListener(updateLobbyInfo);
        LobbyManager.Instance.e_startGame.AddListener(delegate {toggleJoin(); backButton.SetActive(false);});
    }
    #region changing menus
    public void goBack(){
        if(previous == null) return;
        //if(current == u_skilltree) GameObject.Find("SkillTree").GetComponent<SkillTree>().Save();
        current.SetActive(false);
        previous.SetActive(true);
        if(current == u_skilltree)
            current.transform.localPosition = Vector3.zero;
        current = previous;
        previous = null;
        swapBack();
    }
    public void goSkillTree(){
        previous = u_mainmenu;
        current = u_skilltree;
        u_skilltree.SetActive(true);
        u_mainmenu.SetActive(false);
        
        swapBack();
    }
    public void goSkillSelect(){
        previous = u_mainmenu;
        current = u_skillSelect;
        u_skillSelect.SetActive(true);
        u_mainmenu.SetActive(false);
        u_skillSelect.transform.Find("SkillSelect").GetComponent<SkillSelectHolder>().Refresh();
        swapBack();
    }
    public void goLobby(){
        previous = u_mainmenu;
        LobbyManager.Instance.Login(u_mainmenu.transform.Find("Menuground/PlayerName").GetComponent<TMP_InputField>().text);
        current = u_lobby;
        u_lobby.SetActive(true);
        u_mainmenu.SetActive(false);
        swapBack();
    }

    #endregion
    private void swapLobby(){
        if(u_joinLobby.activeSelf){
            u_joinLobby.SetActive(false);
            u_inLobby.SetActive(true);
        }else{
            u_joinLobby.SetActive(true);
            u_inLobby.SetActive(false);
        }
    }
    private void swapBack(){
        if(previous != null){
            backButton.SetActive(true);
        }else{
            backButton.SetActive(false);
        }
    }
    private void toggleJoin(){
        //Debug.Log(u_lobby.activeSelf);
        if(u_lobby.activeSelf){
            u_lobby.SetActive(false);
        }else{
            u_lobby.SetActive(true);
        }
    }
    
    //==========Need to subsrcibe this to data change in LobbyManager to update UI
    private void updateLobbyInfo(){
        gameInfo.updateLobbyInfo(LobbyManager.Instance.currentLobby.Name, 
            LobbyManager.Instance.currentLobby.LobbyCode, 
            LobbyManager.Instance.currentLobby.Data["Difficulty"].Value, 
            LobbyManager.Instance.currentLobby.Data["Length"].Value);
        while(playerList.transform.childCount > 0){
            DestroyImmediate(playerList.transform.GetChild(0).gameObject);
        }
        List<Player> players = LobbyManager.Instance.currentLobby.Players;
        
        foreach(Player p in players){
            GameObject panel = Instantiate(playerPanel, parent:playerList.transform);
            Dictionary<string, PlayerDataObject> data = p.Data;
            panel.GetComponent<PlayerPanel>().initializePanel(data["Name"].Value, 1, data["Ready"].Value,false);
            
            
            
        }
        
    }
    public void Quit(){
        Application.Quit();

    }
    #region lobbybuttons
    public async void createLobby(){
        await LobbyManager.Instance.createLobby(lname.text,4-size.value,difficulty.value.ToString(),length.value.ToString(),visibility.isOn);
    }
    public async void leaveLobby(){
        await LobbyManager.Instance.leaveLobby();
    }
    public async void readyUp(){
        await LobbyManager.Instance.updatePlayer("Ready",PlayerDataObject.VisibilityOptions.Public, "Ready");
    }
    public async void joinByCode(){
        await LobbyManager.Instance.joinByCode(joinCode.text);
    }
    #endregion
    public async void refreshLobbies(){
        try
        {
            QueryLobbiesOptions options = new QueryLobbiesOptions();
            options.Count=10;
            options.Filters = new List<QueryFilter>()
            {
                new QueryFilter(
                    field: QueryFilter.FieldOptions.AvailableSlots,
                    op: QueryFilter.OpOptions.GT,
                    value: "0")
            };

            // Order by newest lobbies first
            options.Order = new List<QueryOrder>()
            {
                new QueryOrder(
                    asc: false,
                    field: QueryOrder.FieldOptions.Created)
            };

            QueryResponse lobbies = await Lobbies.Instance.QueryLobbiesAsync(options);

            while(lobbyList.transform.childCount > 0){
                DestroyImmediate(lobbyList.transform.GetChild(0).gameObject);
            }
            if(lobbies.Results != null){
                foreach(Lobby l in lobbies.Results){
                    GameObject panel = Instantiate(lobbyPanel, parent:lobbyList.transform);
                    Dictionary<string, DataObject> data = l.Data;
                    panel.GetComponent<LobbyPanel>().initializePanel(l.Name,l.MaxPlayers-l.AvailableSlots, l.MaxPlayers,l.Data["Difficulty"].Value,l.Data["Length"].Value, l.Id);
                    
                    
                    
                }
            }
            


        }
        catch (LobbyServiceException e)
        {
            Debug.Log(e);
        }
    }
}
