using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.AI.Navigation;
public class GameManager : NetworkBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    
    [SerializeField]
    private CountInCollider startZone;


    [Header("Tile")]
    public NetworkObject tileGen;
    [SerializeField]
    private Vector3 spawnPosition;
    [SerializeField]
    private NavMeshSurface[] navmeshes;

    [HideInInspector]
    public List<GameObject> players = new List<GameObject>();
    [Header("Game Information")]
    public NetworkVariable<float> points = new NetworkVariable<float>();
    private List<NW_Spawner> spawners = new List<NW_Spawner>();
    private int playerCount;

    void Awake(){
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject)    ;
            return;
        } else {
            _instance = this;
        }
        startZone.hit.AddListener(delegate{
            

            if(IsHost){
                GameObject g = Instantiate(tileGen.gameObject, spawnPosition, Quaternion.identity );
                g.GetComponent<NetworkObject>().Spawn();
                points.Value = 1000;
                foreach(NetworkClient go in NetworkManager.Singleton.ConnectedClientsList){
                    go.PlayerObject.GetComponent<PlayerSpawn>().GoToGenClientRpc(spawnPosition);  
                }
                RefreshNavMeshServerRPC();
            }
            
            
            
        });

    }
    [ServerRpc(RequireOwnership = false)]
    public void forceSpawnServerRPC(ServerRpcParams serverRpcParams = default){
        foreach(NW_Spawner sp in spawners){
            sp.ForceSpawn();
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void RefreshNavMeshServerRPC(ServerRpcParams serverRpcParams = default){
        foreach(NavMeshSurface nm in navmeshes){
            nm.BuildNavMesh();
            
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void DespawnObjectServerRPC(NetworkObjectReference gameObject, ServerRpcParams serverRpcParams = default){
        ulong clientId = serverRpcParams.Receive.SenderClientId;
        gameObject.TryGet(out NetworkObject no);
        no.Despawn();
        Destroy(no.gameObject);
    }
    
    [ServerRpc(RequireOwnership = false)]
    public void TryBuyServerRPC(float cost, NetworkObjectReference gameObject, ServerRpcParams serverRpcParams = default){
        
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {   
            if(cost <= points.Value){
                points.Value -= cost;
                gameObject.TryGet(out NetworkObject no);
                no.GetComponent<BuyDoor>().BuyEventClientRpc();
                
            }
        }
    }
    [ServerRpc(RequireOwnership = false)]
    public void AddPointsServerRpc(float add, ServerRpcParams serverRpcParams = default){
        
        var clientId = serverRpcParams.Receive.SenderClientId;
        if (NetworkManager.ConnectedClients.ContainsKey(clientId))
        {   
            points.Value += add;
        }
    }
    

    public void AddSpawner(NW_Spawner spawn){
        spawners.Add(spawn);
    }
    
}
