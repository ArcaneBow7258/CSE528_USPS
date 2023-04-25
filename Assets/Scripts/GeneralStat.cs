using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class GeneralStats : NetworkBehaviour
{
    public Dictionary<STATTYPE,float> bases = new Dictionary<STATTYPE,float>();
    public Dictionary<STATTYPE, float[]> stats = new Dictionary<STATTYPE, float[]>(); //stored as [additive, multiplicative]
    public Dictionary<Buff, float[]> buffs = new Dictionary<Buff,float[]>(); //buffs [time, stacks]
    // Start is called before the first frame update
    public virtual void Awake(){
        foreach(STATTYPE type in Enum.GetValues(typeof(STATTYPE))){
            try{stats.Add(type,new float[]{0,1});}
            catch{
                stats[type] = new float[]{0,1};
            }
            
        }
    }

    public virtual void Update(){
        List<Buff> removeQueue = new List<Buff>();
        //Debug.Log("update");
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
    }
}
