using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum STATTYPE{
    LIFE = 1,
    SHIELD = 2,
    MOVESPEED = 3,
    DAMAGE = 4,

    WEAPON = 10,
    SMG = 11,
    RIFLE = 12,
    SHOTGUN = 13,
    PISTOL = 14,

    CRITICALCHANCE = 60,
    CRITICALMULTI = 61,
    
    MELEE = 20,

    HEAT = 31,
    COLD = 32,
    ELECTRIC = 33,
    ACID = 34,

    CLIP = 40,
    RESERVE = 41 ,
    RELOAD = 42,
    FIRERATE = 43,

    ABILITYDAMAGE = 50,
    COOLDOWN = 51,

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
