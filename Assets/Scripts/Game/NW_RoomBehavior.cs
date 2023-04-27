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
    public LayerMask layer;
    public Vector2 size;
    public Vector3 spacing;
    //public List<GameObject> spawnPoints;
    public int spawnCount =0;
    [SerializeField]
    private WeightedRNG roomgen;
    [Header("Other stuff")]
    private NetworkVariable<RoomStatus> stat = new NetworkVariable<RoomStatus>(writePerm:NetworkVariableWritePermission.Server);
    public GameObject doorPrefab;
    void Awake(){
        roomgen.Awake();
    }
    public override void OnNetworkSpawn(){
        
        stat.OnValueChanged += OnStatChanged;
    }
    public NW_RoomBehavior UpdateRoom(RoomStatus status)
    {
        if(IsServer){
            stat.Value=status;
            for(int i = 0; i < spawnCount; i++){
                GameObject spawned = roomgen.gen();
                bool valid = false;
                GameObject r = Instantiate(spawned);
                Vector3 test = Vector3.zero;
                while(!valid && spawned != null){
                    test = new Vector3(Random.Range(-size[0], size[0]),0,Random.Range(-size[1], size[1]));
                    if(Physics.OverlapBox(test,spacing,Quaternion.identity,layer).Length > 0){
                        
                        continue;
                    }
                    else{       
                        r.transform.position = test;
                        valid = true;
                    }
                    
                }
                r.GetComponent<NetworkObject>().Spawn();
                r.GetComponent<NetworkObject>().TrySetParent(transform, false);
                
                r.transform.rotation = Quaternion.Euler(0,Random.Range(0,360),0);  
            }
        }
        return this;
    }
    private void OnStatChanged(RoomStatus p , RoomStatus c){
        bool access = false;
        for (int i = 0; i < c.side.Length; i++) 
        {
            access = access || c.side[i];
            sides[i].first.SetActive(!c.side[i]); //wall
            sides[i].second.SetActive(c.side[i]); // door
            if((i == 0 || i == 2) && c.side[i] && IsServer){ 
                GameObject g = Instantiate(doorPrefab);
                
                if(i== 0){
                    g.transform.position += new Vector3(0,1.25f,5);
                    
                }
                else{
                    g.transform.position += new Vector3(5,1.25f,0);
                    g.transform.rotation = Quaternion.Euler(0,90,0);
                }
                g.GetComponent<NetworkObject>().Spawn();
                g.GetComponent<NetworkObject>().TrySetParent(transform, false);
                } //doors onl exists going up or right.
        }
        if(!access && IsServer){
            GetComponent<NetworkObject>().Despawn();
            return;
        }
    }
}

[System.Serializable]
public struct GameObject_Pair{
    public GameObject first;
    public GameObject second;
}