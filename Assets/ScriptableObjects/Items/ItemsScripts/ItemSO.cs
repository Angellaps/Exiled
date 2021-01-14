using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    CraftMaterialItem,
    FoodItem,
    WeaponItem, //i ola ta weapon/shield/armor se ena equipment
    ShieldItem,
    ArmorItem,
    RecipeItem,
    NewItem
}
public abstract class ItemSO : ScriptableObject
{
    public GameObject itemPrefab;
    public Sprite itemUISprite;
    public ItemType itemType;
    public int itemID;
    public int maxAmount;
    public bool isStackable;
    [TextArea(10, 20)]
    public string itemDescription;
}
