using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCraftingMenu : MonoBehaviour
{
    public GameObject craftUI;
    public GameObject craftTextUI;
    bool craftUIEnabled;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            craftTextUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.C)&&craftUIEnabled)
            {
                craftUI.SetActive(true);
                craftUIEnabled = false;
            }
            else if(Input.GetKeyDown(KeyCode.C) && !craftUIEnabled)
            {
                craftUI.SetActive(false);
                craftUIEnabled = true;
            }
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            craftUI.SetActive(false); craftUI.SetActive(false);
            craftUIEnabled = true;
            craftTextUI.SetActive(false);
        }
    }
}
