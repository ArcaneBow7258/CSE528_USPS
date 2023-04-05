using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
//Wrapper for SkillTree using SKillGraph as a base.
public class SkillGraphScene : SceneGraph<SkillGraph>
{
    public static SkillTree tree;
    public GameObject display;
    public GameObject prefab;
    public int pp;
    
    
    
    public SortedList<int,talent> allTalents = new SortedList<int,talent>();
    public List<talent> playerTalents = new List<talent>();
    public void Awake(){
        
        tree = new SkillTree(display, prefab, pp);
        graph.InitTree(tree);
        
        
    
        
    }
    public void Start(){
        
        
    }
}