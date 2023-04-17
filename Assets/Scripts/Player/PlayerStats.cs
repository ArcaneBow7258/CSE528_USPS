using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Dictionary<string,float> bases = new Dictionary<string,float>();
    public Dictionary<string, float> multipliers = new Dictionary<string, float>();
    public Dictionary<string, float> flat = new Dictionary<string, float>();
    public Dictionary<string, bool> flags = new Dictionary<string, bool>();
    public List<SpecialStats> notable = new List<SpecialStats>();
    public SkillTree tree;

    public void Awake(){
        tree = GameObject.Find("SkiillTree").GetComponent<SkillTree>();
        //add stats to player
        tree.AggregateStats();

        //bind abilites
        //tree.equippedAbilities;
    }




}
public class SpecialStats{

}
