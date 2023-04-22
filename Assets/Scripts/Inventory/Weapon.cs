using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Weapon")]
public class Weapon : Item
{
    public enum WeaponType
    { 
        Ranged,
        Melee,
        Other
    }
    public WeaponType weaponType;
    public int ammoCount;
    public int weaponDamage;


    public override Item GetItem() { return this; }
    public override Misc GetMisc() { return null; }
    public override Weapon GetWeapon() { return this; }
    public override Equipment GetEquipment() { return null; }
    public override Consumable GetConsumable() { return null; }

    public Weapon()
    {
        weaponType = WeaponType.Other;
        ammoCount = int.MaxValue;
        weaponDamage = 0;
    }

    public Weapon(WeaponType type, int ammoCapacity, int damage)
    {
        weaponType = type;
        ammoCount = ammoCapacity;
        weaponDamage = damage;
    }

}
