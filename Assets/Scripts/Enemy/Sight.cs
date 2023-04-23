using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;
public class Sight : NetworkBehaviour
{
    public float distance;
    public float angle;
    public LayerMask objectLayers;
    public LayerMask blockLayers;
    public GameObject detectedObject;
    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        if(IsServer){
            Collider[] colliders = Physics.OverlapSphere(
                transform.position, distance, objectLayers
            );
            detectedObject = null;
            List<GameObject> legal = new List<GameObject>();
            for (int i = 0; i < colliders.Length; i++){
                Collider collider = colliders[i];
                Vector3 directionToController = Vector3.Normalize(
                    collider.bounds.center - transform.position
                );
                float angleToCollider = Vector3.Angle(transform.forward, directionToController);
                //Debug.Log(angleToCollider);
                
                if (angleToCollider < angle){
                    if(!Physics.Linecast(transform.position, collider.bounds.center, out RaycastHit hit, blockLayers))
                    {
                        
                        legal.Add(collider.gameObject);
                        //Debug.Log("enemy found player");
                    }else{
                        Debug.DrawLine(transform.position, hit.point, Color.red);
                        //detectedObject = null;
                    }
                }
            }
            if(legal.Count > 0){
                legal.Sort(delegate(GameObject a, GameObject b){ 
                    return Vector3.Distance(transform.position, a.transform.position).CompareTo(Vector3.Distance(transform.position, b.transform.position));
                    });
                detectedObject = legal[0];
                Debug.DrawLine(transform.position, detectedObject.transform.position, Color.green);
            }
        }
        
    }
    public void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, distance);

            Vector3 rightDirection= Quaternion.Euler(0, angle, 0) * transform.forward;
            Gizmos.DrawRay(transform.position, rightDirection * distance);
            Vector3 leftDirection= Quaternion.Euler(0, -angle, 0) * transform.forward;
            Gizmos.DrawRay(transform.position, leftDirection * distance);
        }
}
