using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Sight : MonoBehaviour
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
        Collider[] colliders = Physics.OverlapSphere(
            transform.position, distance, objectLayers
        );
        detectedObject = null;
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
                    Debug.DrawLine(transform.position, collider.bounds.center, Color.green);
                    detectedObject = collider.gameObject;
                    //Debug.Log("enemy found player");
                    break;
                }else{
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    //detectedObject = null;
                }
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
