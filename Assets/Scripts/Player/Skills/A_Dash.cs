using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ActiveAbility/Dash", fileName ="A_Dash")]
public class A_Dash : ActiveAbility{
    public float force;
    public float distance;
    public override void activate(GameObject owner, PlayerStats stats){
        Rigidbody rb = owner.GetComponent<Rigidbody>();
        if(rb != null){
            //Debug.Log("Dash!");
            Vector3 direction = new Vector3(force * Input.GetAxis("Horizontal"), 0, force* Input.GetAxis("Vertical"));
            owner.GetComponent<Fragsurf.Movement.SurfCharacter>().StartCoroutine(owner.GetComponent<Fragsurf.Movement.SurfCharacter>().dashing(rb,distance, direction));
        }
    }
}
