using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public InventorySO playerInventory;
    public GameObject inventoryUI;
    public GameObject menuUI;
    bool inventoryUIEnabled = false;

    public void Update()
    {
        if (inventoryUIEnabled)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                inventoryUI.SetActive(false);
                inventoryUIEnabled = false;
            }
        }
        else if (!inventoryUIEnabled)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                inventoryUI.SetActive(true);
                inventoryUIEnabled = true;
            }
        }
    }
    public void OnReloadSceneButton()
    {
        SceneManager.LoadScene("Main Game Scene");
        playerInventory.InventoryContainer.Clear();
    }
    public void LoadMenu()
    {
        menuUI.SetActive(true);
    }
    public void QuitMenu()
    {
        menuUI.SetActive(false);
    }
}


