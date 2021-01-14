using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Craft Material")]
public class CraftMaterialSO : ItemSO
{
    
    private void Awake()
    {
        itemType = ItemType.CraftMaterialItem;
    }
}
