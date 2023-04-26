using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
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

public class PlayerStats : GeneralStats
{
    
    public Dictionary<string, bool> flags = new Dictionary<string, bool>();
    public List<SpecialStats> notable = new List<SpecialStats>();
    
    public SkillTree tree;

    public override void Awake(){
        base.Awake();
        
        tree = GameObject.FindObjectsOfType<SkillTree>(true)[0].GetComponent<SkillTree>();
        //add stats to player
        
        //bind abilites
        //tree.equippedAbilities;
    }

    public override void OnNetworkSpawn()
    {
        
        if(!IsOwner) return;
        if(tree != null){
            tree.AggregateStats(this);
        }
        base.OnNetworkSpawn();
        
    }
    public override void Update()
    {
        base.Update();
        
    }
}
public class SpecialStats{

}
