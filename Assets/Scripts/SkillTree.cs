using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class SkillTree : MonoBehaviour
{
    public static SkillTree Instance;
    public SkillGraph graph;
    public GameObject display;
    public GameObject prefab;
    public int pp;
    
    
    
    public SortedList<int,talent> allTalents = new SortedList<int,talent>();
    public List<talent> playerTalents = new List<talent>();
    public void Awake(){
        
        if(Instance != null){
            Debug.Log("TWO Skil trees bakana");
        }else{Instance = this;}
        
        graph.InitTree();
        
        
    
        
    }
    public void Start(){
        
        
    }
}
