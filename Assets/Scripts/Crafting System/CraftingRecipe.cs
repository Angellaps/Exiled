using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct ItemAmount
{
    public ItemSO Item;
    [Range(1, 1001)]
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipe : ScriptableObject
{

    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;
    [SerializeField]
    private AudioClip inventoryAddClip;
    [SerializeField]
    GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public bool CanCraft(IItemContainer itemContainer)
    {
        foreach (ItemAmount itemAmount in Materials)
        {
            if (itemContainer.ItemCount(itemAmount.Item) < itemAmount.Amount)
            {
                return false;
            }
        }
        return true;
    }
    public void Craft(IItemContainer itemContainer)
    {
        Debug.Log("item crafted");
        if (CanCraft(itemContainer))
        {
            //Remove materials from bag
            foreach (ItemAmount itemAmount in Materials)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemContainer.RemoveItem(itemAmount.Item);
                    //if we need remove sounds for smthing other than crafted items
                    //AudioSource.PlayClipAtPoint(inventoryRemoveClip, transform.position);
                }
            }
            //Add crafted results in bag
            foreach (ItemAmount itemAmount in Results)
            {
                for (int i = 0; i < itemAmount.Amount; i++)
                {
                    itemContainer.AddItem(itemAmount.Item, itemAmount.Amount);
                    AudioSource.PlayClipAtPoint(inventoryAddClip, player.transform.position);

                }
            }
        }
    }
}

