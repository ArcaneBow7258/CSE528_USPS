using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PlayerSpawn : NetworkBehaviour
{
    //public List<NetworkObject> spawns;
    public Vector3 offset;
    public MeshRenderer mr;
    public List<Color> colors;
    public override void OnNetworkSpawn(){
        if(IsOwner){
            Debug.Log("pog champion spaawned");
            base.OnNetworkSpawn();
            transform.position = offset;
            //spawns[0].Despawn();
            mr.material.color = colors[(int)OwnerClientId];
            gameObject.name = "Player_"+OwnerClientId;
        }
    }
}
