using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Buff/ParcelProtection", fileName = "B_PP")]
public class b_ParcelProtection : Buff
{
    public float shield;
    public override void apply(GeneralStats stats)
    {
        
        //Debug.Log("Add " +  this.name);
        if(!stats.buffs.TryAdd(this,new float[]{durationMax, 1}) && stackable){
            stats.buffs[this][1] += 1;
            //Debug.Log("Stacking");
        };
        stats.ChangeShieldClientRpc(shield, shield);
        
    }

    public override void unapply(GeneralStats stats)
    {
        //Debug.Log("Remove " +  this.name);
        //hope your script auto clamps
        stats.ChangeShieldClientRpc(-shield, -shield);
    }

    public override void trigger(GeneralStats stats)
    {}
}