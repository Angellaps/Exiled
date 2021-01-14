using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/New Inventory")]
public class InventorySO : ScriptableObject
{
    public List<InventorySlot> InventoryContainer = new List<InventorySlot>();
    public void AddItem(ItemSO item, int amount)
    {
        bool itemExists = false ;
        for(int i = 0; i < InventoryContainer.Count; i++)
        {
            if (InventoryContainer[i].item==item)
            {
                InventoryContainer[i].AddAmount(amount);
                itemExists = true;
                break;
            }
        }
        if (!itemExists)
        {
            InventoryContainer.Add(new InventorySlot(item, amount));
        }
    }
}
[System.Serializable]
public class InventorySlot 
{
    public ItemSO item;
    public int itemAmount;
    public InventorySlot(ItemSO item, int amount)
    {
        this.item = item;
        itemAmount = amount;
    }
    public void AddAmount(int value)
    {
        itemAmount += value;
    }
}
