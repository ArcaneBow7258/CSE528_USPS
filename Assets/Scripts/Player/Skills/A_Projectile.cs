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
        /*
        foreach(STATTYPE t in stats.stats.Keys){
            modified += stats.stats[t][0];
        }
        foreach(STATTYPE t in stats.stats.Keys){
            modified *= 1+stats.stats[t][1];
        }*/


        Camera cam = owner.transform.GetComponentInChildren<Camera>();
        Ray target = cam.ScreenPointToRay(Input.mousePosition); //swap 
        if(ray){    
            //Debug.Log(target);
            //Debug.Log(target.GetPoint(range));
            if(Physics.Linecast(owner.transform.position,target.GetPoint(range), out RaycastHit hit, mask )){
                Debug.Log(hit.collider.gameObject.name);
            }else{
                Debug.Log("Miss");
            }
            
          
        }
        else{
            GameObject g = GameObject.Instantiate(prefab);
            g.transform.forward = target.direction;
            
            }
    }
}
