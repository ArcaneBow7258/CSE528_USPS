using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbility : ScriptableObject
{
    public GameObject owner;
    public abstract void trigger();
    
}


