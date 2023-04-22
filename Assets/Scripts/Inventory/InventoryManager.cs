using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.CSharp;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventorySlotHolder;
    [SerializeField] private GameObject weaponSlotHolder;
    [SerializeField] private GameObject equipmentSlotHolder;
    [SerializeField] private Item itemToAdd;
    [SerializeField] private Item itemToRemove;

    [SerializeField] private Slot[] startingItems;

    private Slot[] items;
    private Slot[] weapons;
    private Slot[] equipment;

    private GameObject[] inventorySlots;
    private GameObject[] weaponSlots;
    private GameObject[] equipmentSlots;

    private Slot movingSlot;
    private Slot tempSlot;
    private Slot originalSlot;
    private bool isMovingItem;


    private void Start()
    {

        inventorySlots = new GameObject[inventorySlotHolder.transform.childCount];
        weaponSlots = new GameObject[weaponSlotHolder.transform.childCount];
        equipmentSlots = new GameObject[equipmentSlotHolder.transform.childCount];

        items = new Slot[inventorySlots.Length];
        weapons = new Slot[weaponSlots.Length];
        equipment = new Slot[equipmentSlots.Length];

        #region Array Initialization
        for (int i = 0; i < inventorySlotHolder.transform.childCount; i++)
        {
            inventorySlots[i] = inventorySlotHolder.transform.GetChild(i).gameObject;
        }

        for(int i = 0; i < weaponSlotHolder.transform.childCount; i++)
        {
            weaponSlots[i] = weaponSlotHolder.transform.GetChild(i).gameObject;
        }

        for(int i = 0; i < equipmentSlotHolder.transform.childCount; i++)
        {
            equipmentSlots[i] = equipmentSlotHolder.transform.GetChild(i).gameObject;
        }

        for(int i = 0; i < items.Length; i++)
        {
            items[i] = new Slot();
        }

        for(int i = 0; i < weapons.Length; i++)
        {
            weapons[i] = new Slot();
        }

        for(int i = 0; i < equipment.Length; i++)
        {
            equipment[i] = new Slot();
        }

        for(int i = 0; i < startingItems.Length; i++)
        {
            items[i] = startingItems[i];
        }
        #endregion

        Add(itemToAdd, 1);
        RefreshUI();

    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)) // Clicked
        {
            //Find Closest Slot or slot we clicked on
            /*Debug.Log(GetClosestSlot().GetItem());*/
            if(isMovingItem)
            {
                //Drop item
                EndItemMove();
            }
            else
            {
                BeginItemMove();
            }
        }
    }

    #region Inventory Utils
    public void RefreshUI()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            try
            {
                inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = items[i].GetItem().itemIcon;
                inventorySlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                if(items[i].GetItem().isStackable)
                {
                    inventorySlots[i].transform.GetChild(1).GetComponent<Text>().text = items[i].GetQuantity().ToString();
                }
                else
                {
                    inventorySlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
                }
            }
            catch
            {
                inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                inventorySlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                inventorySlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }

        for (int i = 0; i < weaponSlots.Length; i++)
        {
            try
            {
                weaponSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = weapons[i].GetItem().itemIcon;
                weaponSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                if (weapons[i].GetItem().GetWeapon() != null)
                {
                    weaponSlots[i].transform.GetChild(1).GetComponent<Text>().text = weapons[i].GetItem().GetWeapon().ammoCount.ToString();
                }
            }
            catch
            {
                weaponSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                weaponSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                weaponSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";

            }
        }

        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            try
            {
                equipmentSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = equipment[i].GetItem().itemIcon;
                equipmentSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                equipmentSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
            catch
            {
                equipmentSlots[i].transform.GetChild(0).GetComponent<Image>().sprite = null;
                equipmentSlots[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                equipmentSlots[i].transform.GetChild(1).GetComponent<Text>().text = "";
            }
        }
    }

    public bool Add(Item item, int quantity)
    {
        //items.Add(item);

        //Check if duplicate item exists
        Slot slot = Contains(item);
        if(slot != null && slot.GetItem().isStackable)
        {
            slot.SetQuantity(slot.GetQuantity() + 1);
        }
        else
        {
            for(int i = 0; i < items.Length; i++)
            {
                if(items[i].GetItem() == null)
                {
                    items[i].AddItem(item, 1);
                    break;
                }
            }
        }

        RefreshUI();
        return true;
    }

    public bool Remove(Item item)
    {
        //items.Remove(item);
        Slot temp = Contains(item);
        if(temp != null)
        {
            if(temp.GetQuantity() > 1)
            {
                temp.SetQuantity(temp.GetQuantity() - 1);
            }
            else
            {
                int slotToRemoveIndex = 0;
                for(int i = 0; i < items.Length; i++)
                {
                    if(items[i].GetItem() == item)
                    {
                        slotToRemoveIndex = i;
                        break;
                    }
                }
                items[slotToRemoveIndex].Clear();
            }
        }
        else
        {
            return false;
        }

        RefreshUI();
        return true;

    }

    public Slot Contains(Item item)
    {
        foreach(Slot slot in items)
        {
            if(slot.GetItem() == item)
            {
                return slot;
            }
        }

        return null;
    }
    #endregion

    #region Dragging
    private Slot GetClosestSlot()
    {
        Slot result = null;
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (Vector2.Distance(inventorySlots[i].transform.position, Input.mousePosition) <= 60)
            {
                result = items[i];
                break;
            }
        }

        for(int i = 0; i < weaponSlots.Length; i++)
        {
            if (Vector2.Distance(weaponSlots[i].transform.position, Input.mousePosition) <= 60)
            {
                result = weapons[i];
                break;
            }
        }
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (Vector2.Distance(equipmentSlots[i].transform.position, Input.mousePosition) <= 60)
            {
                result = equipment[i];
                break;
            }
        }

        return result;
    }

    private bool BeginItemMove()
    {
        originalSlot = GetClosestSlot();
        if(originalSlot == null || originalSlot.GetItem() == null)
        {
            return false;
        }

        movingSlot = new Slot(originalSlot);
        originalSlot.Clear();
        RefreshUI();
        isMovingItem = true;
        return true;
    }

    private bool EndItemMove()
    {
        originalSlot = GetClosestSlot();

        if(originalSlot == null)
        {
            Add(movingSlot.GetItem(), movingSlot.GetQuantity());
            movingSlot.Clear();
        }
        else
        {
            if (originalSlot.GetItem() != null)
            {
                if (originalSlot.GetItem() == movingSlot.GetItem()) // Same Item (They should stack)
                {
                    if (originalSlot.GetItem().isStackable)
                    {
                        originalSlot.SetQuantity(movingSlot.GetQuantity() + 1);
                        movingSlot.Clear();
                    }
                    else
                    {
                        return false;
                    }
                }
                
                else // Different Item (Swap)
                {
                    tempSlot = new Slot(originalSlot); 
                    originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity()); 
                    movingSlot.AddItem(tempSlot.GetItem(), tempSlot.GetQuantity()); 
                    RefreshUI();
                    return true;
                }
            }
            else // Move item as usual
            {
                originalSlot.AddItem(movingSlot.GetItem(), movingSlot.GetQuantity());
                movingSlot.Clear();
            }
        }
        isMovingItem = false;
        RefreshUI();
        return true;
    }
    #endregion
}