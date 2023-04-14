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
    public void Start(){
        
        
    }
}