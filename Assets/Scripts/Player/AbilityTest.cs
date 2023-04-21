using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTest : MonoBehaviour
{
    public ActiveAbility a1;
    public ActiveAbility a2;

    PlayerStats stats;

    void Awake(){
        stats = GetComponent<PlayerStats>();
        a1.Init(gameObject);
        a2.Init(gameObject);
    }
    // Update is called once per frame
    void Update()
    {   
        
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            //Debug.Log("a1");
            a1.activate();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            //Debug.Log("a2");
            a2.activate();
        }
    }
}
