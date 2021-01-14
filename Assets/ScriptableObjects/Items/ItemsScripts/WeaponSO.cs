using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Weapon Item")]
public class WeaponSO : ItemSO
{
    public int attackBonusValue;
    public int durability;
    public string rarity;
    private void Awake()
    {
        itemType = ItemType.WeaponItem;
    }
}
