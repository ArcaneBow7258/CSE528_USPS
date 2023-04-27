using System;
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
    [Tooltip("Information about character above you")]
    public TMP_Text topbar;
    public GameObject hud;
    private NetworkVariable<FixedString128Bytes> networkPlayerName = new NetworkVariable<FixedString128Bytes>("placeholder", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public override void OnNetworkSpawn(){
        
        //Debug.Log("pog champion spaawned");
        base.OnNetworkSpawn();
        //spawns[0].Despawn();
        mr.material.color = colors[(int)OwnerClientId];
        transform.Find("MipMapMarker").GetComponent<MeshRenderer>().material.color =  colors[(int)OwnerClientId];
        GameManager.Instance.players.Add(gameObject);
        //Create the topbar locally, except ofr yourself
        if(IsOwner) {transform.position = offset;
            //topbar.transform.parent.gameObject.SetActive(false);
            Debug.Log("I own this object");
        }else{
            hud.gameObject.SetActive(false);
            Destroy(transform.Find("MipMapCamera").gameObject);
        }
    }

    public async void Awake(){
        await LobbyManager.Instance.updatePlayer("ClientID", PlayerDataObject.VisibilityOptions.Member,NetworkManager.Singleton.LocalClientId.ToString());
        if(IsOwner){
            
            
        } 
        //}
    }
    public void Start(){
        
    }
    public void Update(){
        if(networkPlayerName.Value == "placeholder"){
            if(IsOwner){//polling to geet this update sigh
                try{
                networkPlayerName.Value = LobbyManager.Instance.currentLobby.Players.Where((p) => {return p.Data["ClientID"].Value.Equals(NetworkManager.Singleton.LocalClientId.ToString());}).First().Data["Name"].Value;
                topbar.text = networkPlayerName.Value.ToString();
                }
                catch{}
            }
            topbar.text = networkPlayerName.Value.ToString();
        }
    }
    [ClientRpc]
    public void GoToGenClientRpc(Vector3 spawnPosition){
        transform.position = spawnPosition + new Vector3(0,2,0);
    }
}
