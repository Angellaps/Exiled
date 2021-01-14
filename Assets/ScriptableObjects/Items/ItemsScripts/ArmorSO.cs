using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Armor Item")]
public class ArmorSO : ItemSO
{
    public int defenseValue;
    public int durability;
    public string equipmentSlot;
    public string rarity;
    private void Awake()
    {
        itemType = ItemType.ArmorItem;
    }
}
