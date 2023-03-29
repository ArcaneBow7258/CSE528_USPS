using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;
public class TestAttach : NetworkBehaviour
{
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
            
            gameObject.transform.Translate(new Vector3(0,5*Time.deltaTime,0));
        }
    }
}
