using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class SkillTreeDisplay : MonoBehaviour
{
    
    public float zoomSpeed;
    public float minZoom;
    public float maxZoom;
    public float panSpeed;
    public float dragSpeed;

    private float zooming;
    private Vector2 panning;
    private float dragging;
    
    
    void OnPan(InputValue value){
        panning = value.Get<Vector2>();
        //Debug.Log(val*Time.deltaTime*panSpeed);
        

    }
    void OnZoom(InputValue value){
        float val = value.Get<float>();
        if(val> 0){
            Vector3 newScale = transform.localScale + new Vector3(zoomSpeed*4*Time.deltaTime, 
                                                zoomSpeed*4*Time.deltaTime, 
                                                zoomSpeed*4*Time.deltaTime);
            newScale = new Vector3(Mathf.Clamp(newScale.x, minZoom, maxZoom), Mathf.Clamp(newScale.y, minZoom, maxZoom),Mathf.Clamp(newScale.z, minZoom, maxZoom));
            transform.localScale = newScale;
        }else{
            Vector3 newScale = transform.localScale - new Vector3(zoomSpeed*Time.deltaTime, 
                                                zoomSpeed*Time.deltaTime, 
                                                zoomSpeed*Time.deltaTime);
            newScale = new Vector3(Mathf.Clamp(newScale.x, minZoom, maxZoom), Mathf.Clamp(newScale.y, minZoom, maxZoom),Mathf.Clamp(newScale.z, minZoom, maxZoom));
            transform.localScale = newScale;
        }
    }
    public void Update(){
        //need to clmap later
        transform.Translate(-panning.x*panSpeed*Time.deltaTime,-panning.y*panSpeed*Time.deltaTime,0);
        
        
    }
  
}