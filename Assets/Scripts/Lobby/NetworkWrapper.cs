using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class NetworkWrapper : MonoBehaviour
{
    public static GameObject I(GameObject g){
        GameObject spawned  = Instantiate(g);
        spawned.GetComponent<NetworkObject>().Spawn();
        return spawned;
    }
    public static GameObject I(GameObject g, Transform t){
        GameObject spawned  = Instantiate(g, t);
        spawned.GetComponent<NetworkObject>().Spawn();
        return spawned;
    }
    public static GameObject I(GameObject g, Vector3 t, Quaternion q){
        GameObject spawned  = Instantiate(g,t,q);
        spawned.GetComponent<NetworkObject>().Spawn();
        return spawned;
    }
    public static GameObject I(GameObject g, Vector3 t, Quaternion q, Transform p){
        GameObject spawned  = Instantiate(g,t,q, p);
        spawned.GetComponent<NetworkObject>().Spawn();
        return spawned;
    }
}
