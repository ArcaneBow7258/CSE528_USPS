using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System;
[Serializable]
public enum WeaponStat{ //consistent with player stats
        WEAPON = 10,
        SMG = 11,
        RIFLE = 12,
        SHOTGUN = 13,
        PISTOL = 14,
        CLIP = 40,
        RESERVE = 41 ,
        RELOAD = 42,
        FIRERATE = 43,
}
[Serializable]
public class Weapon
{
    public WeaponBase based;
    public int ammoReserve;
    public int reserveMax;
    public float weaponDamage;
    public float timePerShot;
    public int magSize;
    public int currentMag;
    public float reloadTime;
    

    public Weapon(WeaponBase weaponBase){
        based = weaponBase;
        ammoReserve = based.ammoReserve;
        reserveMax = based.ammoReserve;
        weaponDamage = based.weaponDamage;
        timePerShot = based.timePerShot;
        magSize = based.magSize;
        currentMag = based.currentMag;
        reloadTime = based.reloadTime;
    }
}
