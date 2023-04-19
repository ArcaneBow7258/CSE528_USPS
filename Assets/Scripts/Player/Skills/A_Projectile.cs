using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName ="ActiveAbility/Projectile", fileName ="A_Projectile")]
public class A_Projectile : ActiveAbility{
    public GameObject prefab;
    public float damage;
    public LayerMask mask;
    public bool ray;
    public float range;
    public override void activate(){
        float modified = damage;
        foreach(STATTYPE t in stats.stats.Keys){
            damage += stats.stats[t][0];
        }
        foreach(STATTYPE t in stats.stats.Keys){
            damage *= 1+stats.stats[t][1];
        }


        Camera cam = owner.transform.GetComponentInChildren<Camera>();
        Ray target = cam.ScreenPointToRay(Input.mousePosition); //swap 
        if(ray){    
            Physics.Linecast(target.origin,target.GetPoint(range), out RaycastHit hit, mask );
        }
        else{
            GameObject g = GameObject.Instantiate(prefab);
            g.transform.forward = target.direction;
            
            }
    }
}
