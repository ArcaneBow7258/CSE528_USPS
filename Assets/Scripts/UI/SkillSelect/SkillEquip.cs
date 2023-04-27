using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SkillEquip : MonoBehaviour, IDropHandler
{
    public SkillTree tree;
    public void OnDrop(PointerEventData eventData)
    {
        if(transform.childCount > 0){
            tree.equippedAbilities.Remove(transform.GetChild(0).GetComponent<SkillDrag>().representing);
            transform.GetChild(0).SetParent(eventData.pointerDrag.transform.parent);
        }
        if(eventData.pointerDrag != null){
            eventData.pointerDrag.GetComponent<RectTransform>().position = transform.position;
            eventData.pointerDrag.transform.SetParent(transform);
            tree.equippedAbilities.Add(eventData.pointerDrag.GetComponent<SkillDrag>().representing);
        }
    }
    public void OnEnable(){
        Transform tr;
        if(transform.childCount >0){
            if((tr = transform.GetChild(0)) != null){
                if(!(tree.equippedAbilities.Contains(tr.GetComponent<SkillDrag>().representing))){
                Destroy(tr.gameObject);
                }
            }
        }
    }
}
