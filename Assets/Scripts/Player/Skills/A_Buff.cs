using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ActiveAbility/Buff", fileName ="A_Buff")]
public class A_Buff : ActiveAbility{
    public Buff buff;
    public override void activate(){
        //add buffer to component
        
        buff.apply(stats);
    }
}
