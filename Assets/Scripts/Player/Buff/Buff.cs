using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(menuName ="Buff", fileName ="A_Projectile")]
public abstract class Buff : ScriptableObject
{   
    public float durationMax;
    public bool stackable;
    
    public abstract void apply(GeneralStats stats);
    public abstract void trigger(GeneralStats stats);
    public abstract void unapply(GeneralStats stats);
}
