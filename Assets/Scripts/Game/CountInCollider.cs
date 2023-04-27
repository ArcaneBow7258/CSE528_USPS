using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.Events;
public class CountInCollider : NetworkBehaviour
{
    public bool single_use;
    private bool use = true;
    public int minPlayers = 4;
    private int detected = 0;
    public UnityEvent hit;
    void Awake(){
    
        enabled = IsHost;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(IsServer){
            minPlayers = Mathf.Min(minPlayers, NetworkManager.Singleton.ConnectedClients.Count);
        }
        detected += 1;
        //Debug.Log("pog");
        if(IsServer){
            if(detected >= minPlayers && use && ( NetworkManager.Singleton.ConnectedClients.Count == LobbyManager.Instance.currentLobby.Players.Count)){
                hit.Invoke();
                if(single_use){
                    use = !use;
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        detected -= 1;
    }

}
