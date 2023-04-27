using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using TMPro;
public class BuyDoor : NetworkBehaviour
{
    public float cost = 500;
    public float cooldown = 1;
    public float cooldownTime = 0;
    public bool dangerDoor = false;
    public TMP_Text text;
    public override void OnNetworkSpawn(){
        base.OnNetworkSpawn();
        text.text = cost.ToString();
    }
    void OnCollisionEnter(Collision collision){
        //if(collision.gameObject.layer != LayerMask.GetMask("Player")) return;
        if(Time.time > cooldownTime){
            cooldownTime = Time.time + cooldown;
        }else{
            return;
        }
        GameManager.Instance.TryBuyServerRPC(cost, gameObject);
       
    }
    [ClientRpc]
    public void BuyEventClientRpc(ClientRpcParams clientRpcParams = default){
        GameManager.Instance.forceSpawnServerRPC();
        GameManager.Instance.DespawnObjectServerRPC(gameObject,default);
        GameManager.Instance.RefreshNavMeshServerRPC();
    }
}
