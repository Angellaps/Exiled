using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipeUI : MonoBehaviour
{
    //fields for each recipe
    [SerializeField]
    public InventorySO playerInventory;
    [SerializeField]
    private CraftingRecipe hammerRecipe;
    [SerializeField]
    private CraftingRecipe silverKeyRecipe;
    [SerializeField]
    private CraftingRecipe goldKeyRecipe;
    [SerializeField]
    private CraftingRecipe UhammerRecipe;
    [SerializeField]
    private CraftingRecipe RhammerRecipe;
    [SerializeField]
    private CraftingRecipe LhammerRecipe;

    //functions for craft button
    public void OnCraftHammerButton()
    {
        if (hammerRecipe.CanCraft(playerInventory))
        {
            hammerRecipe.Craft(playerInventory);
        }
    }
    public void OnCraftSKeyButton()
    {
        if (silverKeyRecipe.CanCraft(playerInventory))
        {
            silverKeyRecipe.Craft(playerInventory);
        }
    }
    public void OnCraftGKeyButton()
    {
        if (goldKeyRecipe.CanCraft(playerInventory))
        {
            goldKeyRecipe.Craft(playerInventory);
        }
    }
    public void OnCraftRhammerButton()
    {
        if (RhammerRecipe.CanCraft(playerInventory))
        {
            RhammerRecipe.Craft(playerInventory);
        }
    }
    public void OnCraftUhammerButton()
    {
        if (UhammerRecipe.CanCraft(playerInventory))
        {
            UhammerRecipe.Craft(playerInventory);
        }
    }
    public void OnCraftLhammerButton()
    {
        if (LhammerRecipe.CanCraft(playerInventory))
        {
            LhammerRecipe.Craft(playerInventory);
        }
    }
}
