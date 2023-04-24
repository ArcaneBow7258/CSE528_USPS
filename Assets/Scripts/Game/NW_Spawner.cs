using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
public class NW_Spawner : NetworkBehaviour
{
    public spawnList[] table;
    public int weight;
    public float timeToSpawn;
    public float lastSpawn;
    public bool DestroyWithSpawner;
    public List<NetworkObject> spawned = new List<NetworkObject>();
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsServer;
        if(!enabled) return;

    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if(IsServer && DestroyWithSpawner){
            foreach(NetworkObject s in spawned){
                NetworkObjectPool.Singleton.ReturnNetworkObject(s,s.gameObject);
            }
        }
    }
    public void Start(){
        //lastSpawn = Time.time;
    }
    public void Update(){
        if( Time.timeScale != 0 && (Time.time >= lastSpawn + timeToSpawn)){
            NetworkObject g = NetworkObjectPool.Singleton.GetNetworkObject(table[0].prefab, transform.position + new Vector3(0,table[0].prefab.transform.localScale.y,0), transform.rotation);
            g.Spawn();
            spawned.Add(g);
            g.StartCoroutine(g.GetComponent<EnemyAI>().Deactive(table[0].prefab));
            g.GetComponent<NetworkObject>().TrySetParent(transform);
            lastSpawn = Time.time;
        }
    }

}
[Serializable]
public struct spawnList{
    public GameObject prefab;
    public int chance;
    public int weight;
}
