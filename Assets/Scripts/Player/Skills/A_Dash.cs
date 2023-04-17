using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ActiveAbility/Dash", fileName ="A_Dash")]
public class Dash : ActiveAbility{
    public float force;
    public override void activate(){
        Rigidbody rb = owner.GetComponent<Rigidbody>();
        if(rb != null){
            
        }
    }
}
