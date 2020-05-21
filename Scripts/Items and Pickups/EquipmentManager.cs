using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Equipment manager.
/// </summary>
public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;                                       //Callback method called whenever equipment is changed.

    Equipment[] currentEquipment;
    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip (Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;     //Determines equipment slot for newItem
        Equipment oldItem = null;
        if(currentEquipment[slotIndex] != null)     //If the current slot has an item...
        {
            oldItem = currentEquipment[slotIndex];  //...set old item to that item...
            inventory.Add(oldItem);                 //...and add it back to the inventory.
        }
        if (onEquipmentChanged != null)                 //Invoke callback method when item is equipped.
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;      //Equips newItem in slot [slotIndex]
    }

    /// <summary>
    /// Unequips items.
    /// </summary>
    /// <param name="slotIndex"></param>
    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)                 //Invoke callback method when item is unequipped.
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    /// <summary>
    /// Unequips all equipped items.
    /// </summary>
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }
}
