using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Recipe Item")]
public class RecipeSO : ItemSO
{
    public int requirementAmount;
    public int typeAmount;
    public ItemType requirementType;

    private void Awake()
    {
        itemType = ItemType.RecipeItem;
    }
}
