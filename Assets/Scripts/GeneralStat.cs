using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GeneralStats : NetworkBehaviour
{
    [Header("Stats")]
    public NetworkVariable<float> life = new NetworkVariable<float>(100, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> maxLife = new NetworkVariable<float>(100, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> shield = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> maxShield = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    [Header("Dicks")]
    public Dictionary<STATTYPE,float> bases = new Dictionary<STATTYPE,float>();
    public Dictionary<STATTYPE, float[]> stats = new Dictionary<STATTYPE, float[]>(); //stored as [additive, multiplicative]
    public Dictionary<Buff, float[]> buffs = new Dictionary<Buff,float[]>(); //buffs [time, stacks]
    [Header("Regeneraton")]
    public float shieldDelay = 5;
    private bool shieldRegen = false;
    private float shieldLastHit = 0;
    // Start is called before the first frame update
    public virtual void Awake(){
        foreach(STATTYPE type in Enum.GetValues(typeof(STATTYPE))){
            try{stats.Add(type,new float[]{0,1});}
            catch{
                stats[type] = new float[]{0,1};
            }
            
        }
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(!IsServer) return;
        life.Value = (100 + stats[STATTYPE.LIFE][0])*stats[STATTYPE.LIFE][1];
        maxLife.Value = life.Value;
        shield.Value  = (stats[STATTYPE.SHIELD][0])*stats[STATTYPE.SHIELD][1];
        maxShield.Value = shield.Value;
        shield.OnValueChanged += (float prev, float c) => {
            if(c < prev){
                shieldRegen = false;
                shieldLastHit = Time.time;
            }
        };
    }
    public virtual void Update(){
        List<Buff> removeQueue = new List<Buff>();
        //Debug.Log("update");
        if(!IsOwner) return;
        foreach(Buff b in buffs.Keys){
            Debug.Log(b.name);
            buffs[b][0] -= Time.deltaTime;
            if(buffs[b][0] <= 0){
                b.unapply(this);
                removeQueue.Add(b);
            }
        }
        foreach(Buff b in removeQueue){
            
            buffs.Remove(b);
        }
        if(Time.time > shieldLastHit + shieldDelay){
            shield.Value += Time.deltaTime * (maxShield.Value * 0.1f); //Regenerate 10% of your shield every second
        }
        if(life.Value <= 0){
            GameManager.Instance.DespawnObjectServerRPC(gameObject);
            Destroy(gameObject);
        }
        //Clamping    
        life.Value = Mathf.Min(life.Value, maxLife.Value);
        shield.Value = Mathf.Min(shield.Value, maxShield.Value);
    }
    [ClientRpc]
    public void TakeDamageClientRpc(float damage, ClientRpcParams clientRpcParams = default){
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
