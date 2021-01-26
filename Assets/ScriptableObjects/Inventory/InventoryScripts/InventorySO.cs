using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/New Inventory")]
public class InventorySO : ScriptableObject, IItemContainer
{
    public List<InventorySlot> InventoryContainer = new List<InventorySlot>();
    public bool AddItem(ItemSO item, int amount)
    {
        bool itemExists = false;
        for (int i = 0; i < InventoryContainer.Count; i++)
        {
            if (InventoryContainer[i].item == item)
            {
                InventoryContainer[i].AddAmount(amount);
                itemExists = true;
                return true;
                //break;
            }
        }
        if (!itemExists)
        {
            InventoryContainer.Add(new InventorySlot(item, amount));
            return true;
        }
        return false;

    }

    public int ItemCount(ItemSO item)
    {
        for (int i = 0; i < InventoryContainer.Count; i++)
        {
            if (InventoryContainer[i].item == item)
            {
                return InventoryContainer[i].itemAmount;
            }

        }
        return -1;
    }

    public bool RemoveItem(ItemSO item)
    {
        bool itemExists = false;
        for (int i = 0; i < InventoryContainer.Count; i++)
        {
            if (InventoryContainer[i].item == item)
            {
                InventoryContainer[i].DecreaseAmount(1);
                itemExists = true;
                return itemExists;
            }
        }
        /*if (!itemExists)
        {
            InventoryContainer.Add(new InventorySlot(item, 1));
            return true;
        }*/
        return false;
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
    public void DecreaseAmount(int value)
    {
        itemAmount -= value;
    }
}
