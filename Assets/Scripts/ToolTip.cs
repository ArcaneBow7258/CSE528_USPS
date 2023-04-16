using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TMP_Text text;
    public RectTransform tooltip;
    //public Camera uiCam;
    public float padding;
    public void Awake(){
        UpdateText("Place Holder Data\nWhat is going on");
        tooltip.gameObject.SetActive(false);
    }
    public void Start(){
        tooltip.sizeDelta = new Vector2(text.preferredWidth + padding, text.preferredHeight + padding);
        tooltip.SetAsLastSibling();
    }
    public void OnPointerEnter(PointerEventData eventData){
        tooltip.sizeDelta = new Vector2(text.preferredWidth + padding, text.preferredHeight + padding);
        tooltip.gameObject.SetActive(true);
        
    }
    public void OnPointerExit(PointerEventData eventData){
        tooltip.gameObject.SetActive(false);
    }
    public void UpdateText(string stuff){
        text.text = stuff;
        tooltip.transform.localPosition += transform.localPosition;
        tooltip.SetParent(transform.parent,true);
        
        //Debug.Log( text.preferredHeight );
        
        
    }
    public void Update(){
        tooltip.sizeDelta = new Vector2(text.preferredWidth + padding, text.preferredHeight + padding);
        //Vector2 localPoint;
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(gameObject.GetComponent<RectTransform>(), Input.mousePosition, uiCam, out localPoint);
        //tooltip.transform.localPosition = localPoint;
    }
}
