using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
//Data structure
[System.Serializable]
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
        //LobbyManager.Instance.e_startGame.AddListener(AggregateStats);
        //delegate {updatePlayer(aggregateStats)}
        SaveFile loaded = SaveSystem.LoadTree();
        
        if(loaded == null){return;}
        loaded.playerTalents.Sort();
        while(playerTalents.Count !=  loaded.playerTalents.Count){
            
            foreach(int i in loaded.playerTalents){
                allTalents[i].addTalent();
            }
        }
        loaded.equippedAbilities.Sort();
        while(equippedAbilities.Count !=  loaded.equippedAbilities.Count){
            
            foreach(int i in loaded.equippedAbilities){
                equippedAbilities.Add(allTalents[i]);
            }
        }

    }
    public void Save(){
        SaveFile save = new SaveFile(this);
        SaveSystem.SaveTree(save);
    }
    //
    public void getAbilities(){
        playerAbilities.Clear();
        foreach(talent t in playerTalents){
            if(t.type == SKILLTYPE.ABILITY){
                playerAbilities.Add(t);
            }
        }
    }
    public void AggregateStats(PlayerStats ps){
        //equippedAbilities.Clear();
        foreach(talent t in playerTalents){
            if(t.type == SKILLTYPE.PASSIVE){
                foreach(passiveStat passive in t.stat){
                    ps.stats[passive.tag][0] += passive.value[0];
                    ps.stats[passive.tag][1] += passive.value[1];
                } 
            }
        }
    }
}
