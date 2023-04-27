using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
public class NW_Spawner : NetworkBehaviour
{   [Header("Other stuff")]
    public WeightedRNG table;   
    public int maxWeight;
    public float timeToSpawn;
    public float lastSpawn;
    public int packSize = 5;
    [Header("Other stuff")]
    public bool DestroyWithSpawner;
    public List<NetworkObject> spawned = new List<NetworkObject>();
    public void Awake(){
        table.Reset();
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsServer;
        if(!enabled) return;
        GameManager.Instance.AddSpawner(this);

    }

    public override void OnNetworkDespawn()
    {
        /*base.OnNetworkDespawn();
        if(IsServer && DestroyWithSpawner){
            foreach(NetworkObject s in spawned){
                NetworkObjectPool.Singleton.ReturnNetworkObject(s,s.gameObject);
            }
        }*/
    }
    public void Start(){
        //lastSpawn = Time.time;
    }
    public void ForceSpawn(){

    }
    public void Spawn(){
        //NetworkObject g = NetworkObjectPool.Singleton.GetNetworkObject(table[0].prefab, transform.position + new Vector3(0,table[0].prefab.transform.localScale.y,0), transform.rotation);
        GameObject prefab =table.gen();
        Vector2 spawnZone = UnityEngine.Random.insideUnitCircle ;
        float adjustment =  0.5f;
        NetworkObject g = Instantiate(prefab,
                                    transform.position + new Vector3(0,prefab.transform.localScale.y,0) + new Vector3(spawnZone.x*adjustment, 0, spawnZone.y*adjustment), 
                                    transform.rotation).GetComponent<NetworkObject>();
        g.name = g.name.Replace("(Clone)", "");
        g.GetComponent<EnemyStat>().spawner = this;
        g.Spawn();
        spawned.Add(g);
        //g.GetComponent<NetworkObject>().TrySetParent(transform, true);

    }
    public void Update(){
        if( Time.timeScale != 0 && (Time.time >= lastSpawn + timeToSpawn)){
            if(spawned.Count <= packSize){
                Spawn();
            }
            
            lastSpawn = Time.time;
        }
    }

}
