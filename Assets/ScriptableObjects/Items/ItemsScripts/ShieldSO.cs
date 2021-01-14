using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Shield Item")]
public class ShieldSO : ItemSO
{
    public int blockValue;
    public int defenseValue;
    public int durability;
    public string rarity;
    private void Awake()
    {
        itemType = ItemType.ShieldItem;
    }
}
