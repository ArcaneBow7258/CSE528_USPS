using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName ="ActiveAbility/Melee", fileName ="A_Melee")]
public class A_Melee : ActiveAbility{
    public float damage;
    public LayerMask mask;
    public bool rect;
    public Vector3 range;
    public override void activate(GameObject owner, PlayerStats stats){
        List<Collider> hits =new List<Collider>();
        float modified = damage;
        /*foreach(STATTYPE t in stats.stats.Keys){
            damage += stats.stats[t][0];
        }
        foreach(STATTYPE t in stats.stats.Keys){
            damage *= 1+stats.stats[t][1];
        }*/
        if(rect){
            
            hits =new List<Collider>( Physics.OverlapBox(owner.transform.position + new Vector3(0,0,range.z/2), range/2, Quaternion.identity, mask));
        }else{
            Collider [] testHit = Physics.OverlapSphere(owner.transform.position, range.x, mask);
            
            foreach(Collider hit in testHit){
                float angleToCollider = Vector3.Angle(owner.transform.forward, Vector3.Normalize(hit.bounds.center - owner.transform.position));
                if(angleToCollider < range.y){
                    hits.Add(hit);
                }

            }
        }
        foreach(Collider hit in hits){
            //do something
            hit.GetComponent<EnemyStat>().DealDamageServerRpc(modified);
            Debug.Log(hit.gameObject.name);
        }
    }
}
