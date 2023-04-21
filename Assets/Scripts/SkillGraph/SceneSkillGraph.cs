using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
//Wrapper for SkillTree using SKillGraph as a base.
public class SkillGraphScene : SceneGraph<SkillGraph>
{
    public SkillTree tree;

    

    public void Awake(){
        

        graph.InitTree(tree);
        
        
    
        
    }
    [ContextMenu("Remake Tree")]
    public void Remake(){
        tree.pp = 3;
        tree.allTalents.Clear();
        tree.playerTalents.Clear();
        tree.equippedAbilities.Clear();
        
        while(tree.display.transform.childCount > 0){
            DestroyImmediate(tree.display.transform.GetChild(0).gameObject);
        }
        
        graph.InitTree(tree);
    }
    public void Start(){
        
        
    }
}