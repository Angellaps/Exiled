using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Food Item")]
public class FoodSO : ItemSO
{
    public int HealValue;
    private void Awake()
    {
        itemType = ItemType.FoodItem;
    }
}
