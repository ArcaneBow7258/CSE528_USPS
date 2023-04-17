using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum STATTYPE{
    WEAPON,
    SMG,
    RIFLE,
    SHOTGUN,

    FIRE,
    ICE,
    LIGHTNING,

    MOVESPEED,
    DAMAGE,

    AMMO,
    RELOAD,
    FIRERATE,

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
            stats.Add(type,new float[]{0,1});
        }
        
        tree = GameObject.Find("SkiillTree").GetComponent<SkillTree>();
        //add stats to player
        tree.AggregateStats();

        //bind abilites
        //tree.equippedAbilities;
    }




}
public class SpecialStats{

}
