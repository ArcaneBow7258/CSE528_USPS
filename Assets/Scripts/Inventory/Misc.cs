using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tool Class", menuName = "Item/Misc")]
public class Misc : Item
{
    public override Item GetItem() { return this; }
    public override Misc GetMisc() { return this; }
    public override Weapon GetWeapon() { return null; }
    public override Equipment GetEquipment() { return null; }
    public override Consumable GetConsumable() { return null; }
}
