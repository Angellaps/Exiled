using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCraftingMenu : MonoBehaviour
{
    public GameObject craftUI;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            craftUI.SetActive(true);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            craftUI.SetActive(false);

        }
    }
}
