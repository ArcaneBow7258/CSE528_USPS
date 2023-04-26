using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.AI.Navigation;
public class EnableOnStart : NetworkBehaviour
{   
    public GameObject tilegen;
    void Awake(){
        gameObject.SetActive(false);
        LobbyManager.Instance.e_startGame.AddListener(delegate {
            gameObject.SetActive(true);
        });
        RelayManager.Instance.e_relayDone.AddListener(delegate{
            if(NetworkManager.IsServer){
            GameObject r =  Instantiate(tilegen, position:new Vector3(0,4,-5), Quaternion.identity);
            r.GetComponent<NetworkObject>().Spawn();
            
            }else{
                transform.GetComponent<NavMeshSurface>().enabled = false;
            }
        });
    }
}
