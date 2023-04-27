using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item: ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;
    public GameObject itemModel;
    public bool isStackable;

    public abstract Item GetItem();
    public abstract Misc GetMisc();
    public abstract WeaponBase GetWeaponBase();
    public abstract Equipment GetEquipment();
    public abstract Consumable GetConsumable();
}
