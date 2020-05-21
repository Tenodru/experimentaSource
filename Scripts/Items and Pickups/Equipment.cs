using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Framework for Equipment objects.
/// </summary>
[CreateAssetMenu (fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;
    public int armorModifier;
    public int damageModifier;

    /// <summary>
    /// Uses the equipment.
    /// </summary>
    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);      //Feeds this item into EquipmentManager Equip method.
        RemoveFromInventory();
    }
}

/// <summary>
/// Outline for equipment slots.
/// </summary>
public enum EquipmentSlot {  Head, Chest, Legs, Feet, Weapon, Offhand}
