using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class EnemyStat : GeneralStats
{
    public float baseLife;
    public float baseShield;
    public float points = 100;
    [HideInInspector]
    public NW_Spawner spawner;
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        life.Value = baseLife;
        shield.Value = baseShield;
        maxLife.Value = baseLife;
        maxShield.Value = baseShield;
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if(!IsServer) return;
        spawner.spawned.Remove(NetworkObject);
        GameManager.Instance.AddPointsServerRpc(points);
    }
    public override void Update()
    {
        base.Update();

    }
    [ServerRpc(RequireOwnership = false)]
    public void DealDamageServerRpc(float damage, ServerRpcParams serverRpcParams= default){
        if(!IsOwner) return;
        float remainder = damage;
        float placeHolder = shield.Value;
        if(shield.Value > 0){
            shield.Value = Mathf.Max(0, shield.Value - damage);
        }
        damage -= placeHolder;
        if(damage > 0){
            life.Value -= damage;
        }
    }
}
