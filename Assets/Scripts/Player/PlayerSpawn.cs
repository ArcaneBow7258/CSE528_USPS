using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;
using Unity.Collections;
using Unity.Services.Authentication;
using TMPro;
using System.Linq;
public class PlayerSpawn : NetworkBehaviour
{
    //public List<NetworkObject> spawns;
    [Header("Local Information")]
    public Vector3 offset;
    public MeshRenderer mr;
    public List<Color> colors;
    [Header("GUI Information")]
    public Canvas canvas;
    [Tooltip("Information about character above you")]
    public TMP_Text topbar;
    public GameObject hud;
    private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>("Pogchampion", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public override void OnNetworkSpawn(){
        
        //Debug.Log("pog champion spaawned");
        base.OnNetworkSpawn();
        //spawns[0].Despawn();
        mr.material.color = colors[(int)OwnerClientId];
        
        //Create the topbar locally, except ofr yourself
        if(IsOwner) {transform.position = offset;
            
        }
    }

    public async void Awake(){
        enabled = IsLocalPlayer;
        if(IsOwner){
            //LobbyManager.Instance.updatePlayer("GameObject", PlayerDataObject.VisibilityOptions.Member, );
            await LobbyManager.Instance.updatePlayer("ClientID", PlayerDataObject.VisibilityOptions.Member,OwnerClientId.ToString());
        }
    }
    public void Start(){
        if(IsOwner){
            networkPlayerName.Value = LobbyManager.Instance.currentLobby.Players.Where((p) => {return p.Data["ClientID"].Equals(OwnerClientId);}).First().Data["Name"].Value;
            topbar.text = networkPlayerName.Value.ToString();
        }
    }
    [ClientRpc]
    public void GoToGenClientRpc(Vector3 spawnPosition){
        transform.position = spawnPosition + new Vector3(0,2,0);
    }
}
