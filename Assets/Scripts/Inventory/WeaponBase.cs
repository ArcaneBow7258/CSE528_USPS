using System.Collections;
using UnityEngine;

public enum WeaponType
    { 
        Ranged,
        Melee,
        Other
    }

[CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/WeaponBase")]
public class WeaponBase : Item
{
    
    public WeaponType weaponType;
    public int ammoReserve;
    public int reserveMax;
    public float weaponDamage;
    public float timePerShot;
    public int magSize;
    public int currentMag;
    public float reloadTime;
    public bool isAutomatic;
    public Color rayColor;



    public override Item GetItem() { return this; }
    public override Misc GetMisc() { return null; }
    public override WeaponBase GetWeaponBase() { return this; }
    public override Equipment GetEquipment() { return null; }
    public override Consumable GetConsumable() { return null; }

}
