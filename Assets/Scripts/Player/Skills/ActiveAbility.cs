using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ScriptableObject
{
    public GameObject owner;
    public PlayerStats stats;
    public float cooldown; //might delete
    public float cooldownMax;
    public List<STATTYPE> tags = new List<STATTYPE>();
    public void Init(GameObject o){
        owner = o;
        stats = owner.GetComponent<PlayerStats>();
    }

    public abstract void activate();
    
}

