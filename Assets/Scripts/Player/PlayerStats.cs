using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum STATTYPE{
    LIFE,
    SHIELD,

    WEAPON,
    SMG,
    RIFLE,
    SHOTGUN,
    PISTOL,

    HEAT,
    COLD,
    ELECTRIC,
    ACID,

    MOVESPEED,
    DAMAGE,

    CLIP,
    RESERVE,
    RELOAD,
    FIRERATE,

    ABILITYDAMAGE,
    COOLDOWN,
}
public class PlayerStats : MonoBehaviour
{
    public Dictionary<STATTYPE,float> bases = new Dictionary<STATTYPE,float>();
    public Dictionary<STATTYPE, float[]> stats = new Dictionary<STATTYPE, float[]>(); //stored as [additive, multiplicative]
    public Dictionary<string, bool> flags = new Dictionary<string, bool>();
    public List<SpecialStats> notable = new List<SpecialStats>();
    public SkillTree tree;

    public void Awake(){
        foreach(STATTYPE type in Enum.GetValues(typeof(STATTYPE))){
            try{stats.Add(type,new float[]{0,1});}
            catch{
                stats[type] = new float[]{0,1};
            }
            
        }
        
        tree = GameObject.Find("SkillTree").GetComponent<SkillTree>();
        //add stats to player
        tree.AggregateStats(this);

        //bind abilites
        //tree.equippedAbilities;
    }




}
public class SpecialStats{

}
