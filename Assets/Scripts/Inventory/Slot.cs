using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Slot
{
    [SerializeField]
    private Item item;
    [SerializeField]
    private int quantity;

    public Item GetItem()
    {
        return item;
    }

    public void SetItem(Item value)
    {
        item = value;
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public void SetQuantity(int value)
    {
        quantity = value;
    }

    public void Clear()
    {
        this.item = null;
        this.quantity = 0;
    }

    public Slot() 
    {
        item = null;
        quantity = 0;
    }

    public Slot(Slot slot)
    {
        item = slot.item;
        quantity = slot.quantity;
    }

    public Slot(Item _item, int _quantity)
    {
        item = _item;
        quantity = _quantity;
        
    }

    public void AddItem(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public System.Type GetSlotType()
    {
        return item.GetType();
    }
    
}
