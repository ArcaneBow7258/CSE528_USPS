using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;
public class NW_Spawner : NetworkBehaviour
{
    public spawnList[] table;   
    public int maxWeight;
    public float timeToSpawn;
    public float lastSpawn;
    public bool DestroyWithSpawner;
    public List<NetworkObject> spawned = new List<NetworkObject>();
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        enabled = IsServer;
        if(!enabled) return;
        GameManager.Instance.AddSpawner(this);

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
    public void ForceSpawn(){

    }
    public void Spawn(){
        //NetworkObject g = NetworkObjectPool.Singleton.GetNetworkObject(table[0].prefab, transform.position + new Vector3(0,table[0].prefab.transform.localScale.y,0), transform.rotation);
        NetworkObject g = Instantiate(table[0].prefab, transform.position + new Vector3(0,table[0].prefab.transform.localScale.y,0), transform.rotation).GetComponent<NetworkObject>();
        g.name = g.name.Replace("(Clone)", "");
        g.Spawn();
        spawned.Add(g);
        //g.GetComponent<NetworkObject>().TrySetParent(transform, true);

    }
    public void Update(){
        if( Time.timeScale != 0 && (Time.time >= lastSpawn + timeToSpawn)){
            Spawn();
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
