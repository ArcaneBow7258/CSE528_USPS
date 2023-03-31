using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
public class TestAttach : NetworkBehaviour
{

    public int rSpeed;
    public int mSpeed;
    // Start is called before the first frame update
    void Start()
    {
        if(!IsOwner){
            Destroy(gameObject.GetComponent<AudioListener>());
            Destroy(gameObject.transform.GetChild(0).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) return;
        if(Input.GetKey(KeyCode.W)){
            
            gameObject.transform.Translate(new Vector3(0,0,mSpeed*Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.S)){
            
            gameObject.transform.Translate(new Vector3(0,0,-mSpeed*Time.deltaTime));
        }
        if(Input.GetKey(KeyCode.A)){
            gameObject.transform.Rotate(new Vector3(0,-rSpeed*Time.deltaTime,0));
        }
        if(Input.GetKey(KeyCode.D)){
            gameObject.transform.Rotate(new Vector3(0,rSpeed*Time.deltaTime,0));
        }
    }
}
