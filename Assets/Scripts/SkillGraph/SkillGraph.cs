using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class SkillGraph : NodeGraph { 
    SkillNode current;
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
    [ContextMenu("Test")]
    public void Run(){
        Debug.Log("why");
    }
}

