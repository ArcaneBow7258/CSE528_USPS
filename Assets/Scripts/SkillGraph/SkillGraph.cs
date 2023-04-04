using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class SkillGraph : NodeGraph { 
    SkillNode current;
	public void InitTree(){
    for (int i = 0; i < nodes.Count; i++) {
        SkillNode n = nodes[i] as SkillNode;
        n.CreateData();
        }
    for (int i = 0; i < nodes.Count; i++) {
        SkillNode n = nodes[i] as SkillNode;
        n.GetDependencies();
        }
    
    for (int i = 0; i < nodes.Count; i++) {
        SkillNode n = nodes[i] as SkillNode;
        n.Draw();
        }
    }
}

