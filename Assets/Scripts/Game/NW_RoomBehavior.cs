using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class RoomStatus: INetworkSerializable{
    public bool[] side = {false, false, false, false};//corresponds to if  door should be present or not.
    public int distance = 0; //distance from starting room
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref side);
        serializer.SerializeValue(ref distance);
    }


}
public class NW_RoomBehavior : NetworkBehaviour
{   
    [Header("up|down|right|left")]
    [Tooltip("[0] is wall, [1] is door")]
    public GameObject_Pair[] sides;
    public List<GameObject> spawnPoints;
    public List<GameObject_Chance> table;
    private weightedRNG roomgen;
    public NetworkVariable<RoomStatus> stat;
    void Awake(){
        roomgen = new weightedRNG(table);
    }
    public override void OnNetworkSpawn(){
        
        bool access = false;
        for (int i = 0; i < stat.Value.side.Length; i++)
        {
            access = access || stat.Value.side[i];
            sides[i].first.SetActive(!stat.Value.side[i]);
            sides[i].second.SetActive(stat.Value.side[i]);
        }
        if(!access){
            GetComponent<NetworkObject>().Despawn();
            return;
        }
        if(IsServer){
            foreach(var sp in spawnPoints){
                GameObject spawned = roomgen.gen();
                if(spawned != null){
                    GameObject r = Instantiate(spawned, sp.transform);
                    r.GetComponent<NetworkObject>().Spawn();
                    r.GetComponent<NetworkObject>().TrySetParent(transform);
                    
                }
                
            }
        }
    }
    public void UpdateRoom(RoomStatus status)
    {
        stat.Value =  status;
    }
}
[System.Serializable]
public struct GameObject_Pair{
    public GameObject first;
    public GameObject second;
}