using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class SkillDrag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler
{
    public RectTransform rect;
    public CanvasGroup cg;
    public talent representing;
    TMP_Text text;
    public void Awake(){
        rect = GetComponent<RectTransform>();
        cg = GetComponent<CanvasGroup>();
        text = GameObject.Find("SkillDesc").transform.GetChild(0).GetComponent<TMP_Text>();
    }
    public void Init(talent t){
        representing = t;
        //Debug.Log(representing.desc);
        
        GetComponent<Image>().sprite = t.icon;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        cg.blocksRaycasts = false;
        cg.alpha = .6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rect.position += new Vector3(eventData.delta.x , eventData.delta.y, 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        
        cg.blocksRaycasts = true;
        cg.alpha = 1f;
        LayoutRebuilder.MarkLayoutForRebuild(transform.parent.GetComponent<RectTransform>());
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        text.text = representing.name + "\n" + representing.desc;
    }
}
