using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Equipment")]
public class Equipment : Item
{
    public enum EquipmentType
    {
        Shield,
        Boots,
        Helmet,
        Other
    }
    public EquipmentType equipmentType;
    public int effectValue;

    public override Item GetItem() { return this; }
    public override Misc GetMisc() { return null; }
    public override WeaponBase GetWeaponBase() { return null; }
    public override Equipment GetEquipment() { return this; }
    public override Consumable GetConsumable() { return null; }

    public Equipment()
    {
        equipmentType = EquipmentType.Other;
        effectValue = 0;
    }

    public Equipment(EquipmentType type, int effectVal)
    {
        equipmentType = type;
        effectValue = effectVal;
        
    }
}
