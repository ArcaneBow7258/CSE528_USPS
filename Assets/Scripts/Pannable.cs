using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class Pannable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   
    public float panSpeed;
    private Vector2 mouseDelta;
    private bool dragging;
    public void OnPointerUp(PointerEventData eventData){
        dragging = false;
    }

    public void OnPointerDown(PointerEventData eventData){
        dragging = true;
    }
    public void OnMouseMove(InputValue inputValue){
        mouseDelta=inputValue.Get<Vector2>();

    }
    public void Update(){
        if(dragging){
            //square if cool
            transform.Translate(new Vector3(mouseDelta.x, mouseDelta.y) * Time.deltaTime * panSpeed);
        }
    }
}
