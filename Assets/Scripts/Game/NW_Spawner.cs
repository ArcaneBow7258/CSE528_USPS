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
    public List<GameObject> spawned = new List<GameObject>();
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
            foreach(GameObject s in spawned){
                s.GetComponent<NetworkObject>().Despawn();
            }
        }
    }
    public void Start(){
        lastSpawn = Time.time;
    }
    public void Update(){
        if( Time.timeScale != 0 && (Time.time >= lastSpawn + timeToSpawn)){
            GameObject g = Instantiate(table[0].prefab, transform.position, transform.rotation);
            spawned.Add(g);
            g.GetComponent<NetworkObject>().Spawn();
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
