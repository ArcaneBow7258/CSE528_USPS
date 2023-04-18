using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using UnityEditor;
[CreateAssetMenu]
public class SkillGraph : NodeGraph { 
    SkillNode current;
    public Dictionary<STATTYPE,int> highest;
	public void InitTree(SkillTree tree){
        //Debug.Log("creating tree");
        for (int i = 0; i < nodes.Count; i++) {
            SkillNode n = nodes[i] as SkillNode;
            n.CreateData(tree);
            
            }
        // Debug.Log("dependecy tree");
        for (int i = 0; i < nodes.Count; i++) {
            SkillNode n = nodes[i] as SkillNode;
            n.GetLines();
            }
        //Debug.Log("drawing nodes");
        for (int i = 0; i < nodes.Count; i++) {
            SkillNode n = nodes[i] as SkillNode;
            n.Draw();
            }
    }
    [ContextMenu("Collapse All")]
    public void CollapseAll(){
            UnityEngine.Object[] nodes =  Selection.GetFiltered(typeof(PassiveNode), SelectionMode.Editable);
            foreach(PassiveNode n in nodes){
                n.collapse = true;
            }
    }
    [ContextMenu("Opem All")]
    public void OpenAll(){
            UnityEngine.Object[] nodes =  Selection.GetFiltered(typeof(PassiveNode), SelectionMode.Editable);
            foreach(PassiveNode n in nodes){
                n.collapse = false;
            }
    }
    [ContextMenu("Count all")]
    public void CountHigh(){
        if(highest == null){    
            highest=new Dictionary<STATTYPE, int>();
            foreach(STATTYPE type in Enum.GetValues(typeof(STATTYPE))){
                highest.Add(type,0);
                
                
            }
        }
        foreach(SkillNode n in nodes){
            if(n.id >= 1000 && n.type == SKILLTYPE.PASSIVE){
                STATTYPE type = (STATTYPE)(n.id/1000);
                int offset =(n.id%1000);
                if(highest[type] < offset){
                    highest[type] = offset;
                }
            }
        }
    }
}

