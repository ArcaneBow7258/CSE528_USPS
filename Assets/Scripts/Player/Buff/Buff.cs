using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(menuName ="Buff", fileName ="A_Projectile")]
public abstract class Buff : ScriptableObject
{   
    public float duration;
    public float durationMax;
    public bool stackable;
    public int stacks = 1;
    public abstract void apply();
    public abstract void trigger();
    public abstract void remove();
}
