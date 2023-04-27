using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ScriptableObject
{
    public float cooldown = 0; //might delete
    public float cooldownMax = 0;
    public List<STATTYPE> tags = new List<STATTYPE>();

    public abstract void activate(GameObject owner, PlayerStats stats);
    
}

