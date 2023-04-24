using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
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
                foreach(GameObject go in GameObject.FindGameObjectsWithTag("Player")){
                    go.transform.position = spawnPosition + new Vector3(0,10,0);
                }
            }
            
        });

    }
    void FixedUpdate(){
       
    }
}
