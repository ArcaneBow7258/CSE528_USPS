using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class SkillSelectHolder : MonoBehaviour, IDropHandler
{
    public SkillTree s;
    public GameObject prefab;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null){
            eventData.pointerDrag.GetComponent<RectTransform>().position = transform.position;
            eventData.pointerDrag.transform.SetParent(transform);
        }
        s.equippedAbilities.Remove(eventData.pointerDrag.GetComponent<SkillDrag>().representing);
    }

    public void Refresh(){
        s.getAbilities();
        //Debug.Log(transform.childCount);
        GameObject.Find("SkillDesc").transform.GetChild(0).GetComponent<TMP_Text>().text = "";
    
        foreach (Transform child in transform){
            GameObject.Destroy(child.gameObject);
        }
        foreach (talent t in s.playerAbilities){
            if(s.equippedAbilities.Contains(t)){
                continue;
            }
            GameObject g = Instantiate(prefab, parent:transform);
            //Debug.Log(t.icon.name);
            g.GetComponent<SkillDrag>().Init(t);
        }
    }

}
