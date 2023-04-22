using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Consumable")]
public class Consumable : Item
{
    public enum ConsumableType
    {
        Health,
        Shield,
        Other
    }
    public ConsumableType consumableType;
    public int restorationValue;


    public override Item GetItem() { return this; }
    public override Misc GetMisc() { return null; }
    public override Weapon GetWeapon() { return null; }
    public override Equipment GetEquipment() { return null; }
    public override Consumable GetConsumable() { return this; }
}
