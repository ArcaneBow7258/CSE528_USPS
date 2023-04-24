using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;
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
    [Header("Content Generation")]
    [Tooltip("Range in room that you can spawn items")]
    public Vector2 size;
    public Vector3 spacing;
    public List<GameObject> spawnPoints;
    public List<GameObject_Chance> table;
    private weightedRNG roomgen;
    public NetworkVariable<RoomStatus> stat;
    void Awake(){
        roomgen = new weightedRNG(table);
    }
    public override void OnNetworkSpawn(){
        
        
    }
    public void UpdateRoom(RoomStatus status)
    {
        stat.Value =  status;
        bool access = false;
        for (int i = 0; i < stat.Value.side.Length; i++)
        {
            access = access || stat.Value.side[i];
            sides[i].first.SetActive(!stat.Value.side[i]); //wall
            sides[i].second.SetActive(stat.Value.side[i]); // door
            if(i == 0 || i == 2){ DestroyImmediate(sides[i].second.GetComponentInChildren<BuyDoor>().gameObject);} //doors onl exists going up or right.
        }
        if(!access){
            GetComponent<NetworkObject>().Despawn();
            return;
        }
        if(IsServer){
            foreach(var sp in spawnPoints){
                GameObject spawned = roomgen.gen();
                bool valid = false;
                GameObject r = Instantiate(spawned, sp.transform);
                while(!valid && spawned != null){
                    Vector3 test = new Vector3(Random.Range(-size[0], size[0]),0,Random.Range(-size[1], size[1]));
                    if(Physics.OverlapBox(test,spacing,Quaternion.identity,LayerMask.GetMask("Default")).Where(c => c.gameObject.tag == "Obstacle").ToArray().Length > 0){
                        continue;
                    }
                    else{
                        r.transform.localPosition = test;
                        valid = true;
                    }
                    
                    

                }
                r.GetComponent<NetworkObject>().Spawn();
                r.GetComponent<NetworkObject>().TrySetParent(transform);
                r.transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);
                
            }
        }
    }
}
[System.Serializable]
public struct GameObject_Pair{
    public GameObject first;
    public GameObject second;
}