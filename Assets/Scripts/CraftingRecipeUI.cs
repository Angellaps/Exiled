using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{
    [SerializeField]
    public InventorySO playerInventory;
    [SerializeField]
    private CraftingRecipe hammerRecipe;


    public void OnCraftButton()
    {
        if (hammerRecipe.CanCraft(playerInventory))
        {
            hammerRecipe.Craft(playerInventory);
        }
    }
}
