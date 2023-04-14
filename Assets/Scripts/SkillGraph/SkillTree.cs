using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//Data structure
public class SkillTree : MonoBehaviour
{
    public GameObject display;
    public GameObject prefab;
    public int pp;
    
    
    
    public SortedList<int,talent> allTalents = new SortedList<int,talent>();
    public List<talent> playerTalents = new List<talent>();
    public List<talent> playerAbilities = new List<talent>();
    public List<talent> equippedAbilities = new List<talent>();
    

    public void Awake(){
        LobbyManager.Instance.e_startGame.AddListener(AggregateStats);
        //delegate {updatePlayer(aggregateStats)}
    }
    //
    public void AggregateStats(){
        //equippedAbilities.Clear();
        playerAbilities.Clear();
        foreach(talent t in playerTalents){
            if(t.type == SKILLTYPE.PASSIVE){
                //do stats
            }
            if(t.type == SKILLTYPE.ABILITY){
                playerAbilities.Add(t);
            }
        }
    }
    


}
