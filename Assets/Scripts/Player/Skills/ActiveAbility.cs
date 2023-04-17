using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveAbility : ScriptableObject
{
    public GameObject owner;
    public float cooldown;
    public float colldownMax;
    public List<string> tags = new List<string>();


    public abstract void activate();
    
}

