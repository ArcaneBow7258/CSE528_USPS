using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class PointToPlayer : MonoBehaviour
{
    public Camera playerCam;
    [Header("Set 0 or less to make it alwas appear")]
    public float fadeDistance = 5;
    public void Start(){
        if(playerCam == null){
            playerCam = GameObject.Find("Camera").GetComponent<Camera>();
        }
        /*NetworkObject netObject = GetComponent<NetworkObject>();
        netObject.CheckObjectVisibility = ((clientId) => {
            if(fadeDistance <= 0) return true;
            // return true to show the object, return false to hide it
            if (Vector3.Distance(NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject.transform.position, transform.position) < fadeDistance)
            {
                // Only show the object to players that are within 5 meters. Note that this has to be rechecked by your own code
                // If you want it to update as the client and objects distance change.
                // This callback is usually only called once per client
                return true;
            }
            else
            {
                // Hide this NetworkObject
                return false;
            }
        });*/
    }

    // Update is called once per frame
    void Update()
    {   
        if(playerCam == null){
            playerCam = GameObject.Find("Camera").GetComponent<Camera>();
        }
        Vector3 directionToPosition;
        directionToPosition = Vector3.Normalize(playerCam.transform.position - transform.position);
        //directionToPosition.y = 0;
        transform.forward = directionToPosition;
    }
}
