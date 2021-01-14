using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public InventorySO inventory;
    public int horizontalStartPlace;
    public int verticalStartPlace;
    public int horizontalSpace;
    public int verticalSpace;
    public int columns;
    Dictionary<InventorySlot, GameObject> itemsInsideInventory = new Dictionary<InventorySlot, GameObject>();
    void Start()
    {
        CreateInventory();
    }

    void Update()
    {
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        for (int i = 0; i < inventory.InventoryContainer.Count; i++)
        {
            if (itemsInsideInventory.ContainsKey(inventory.InventoryContainer[i]))
            {
                itemsInsideInventory[inventory.InventoryContainer[i]].GetComponentInChildren<TextMeshProUGUI>().text = inventory.InventoryContainer[i].itemAmount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventory.InventoryContainer[i].item.itemPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.InventoryContainer[i].itemAmount.ToString("n0");
                itemsInsideInventory.Add(inventory.InventoryContainer[i], obj);
            }
        }
    }
    public void CreateInventory()
    {
        for (int i = 0; i < inventory.InventoryContainer.Count; i++)
        {
            var obj = Instantiate(inventory.InventoryContainer[i].item.itemPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = inventory.InventoryContainer[i].itemAmount.ToString("n0");
            itemsInsideInventory.Add(inventory.InventoryContainer[i], obj);
        }
    }
   
    public Vector3 GetPosition(int i)
    {
        return new Vector3((horizontalStartPlace+ horizontalSpace*((i%columns))),(verticalStartPlace+(-verticalSpace*(i/columns))),0f);
    }
}
