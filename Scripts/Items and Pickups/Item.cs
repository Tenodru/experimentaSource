using UnityEngine;

/// <summary>
/// Framework for Item objects.
/// </summary>
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";    //Name of item
    public Sprite icon = null;              //Item icon
    public bool isDefaultItem = false;      //Is the item default equipment?

    /// <summary>
    /// Overridable method. Uses the item.
    /// </summary>
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    /// <summary>
    /// Removes item from inventory.
    /// </summary>
    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }
}
