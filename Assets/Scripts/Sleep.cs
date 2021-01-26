using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    public Animator PlayerAnim;
    public UIManager UIManager;
    public GameObject interactPanel;

    /*private void Start()
    {
        //UIManager = FindObjectOfType<UIManager>();
    }*/
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log(UIManager.daysSurvived);
                UIManager.daysSurvived++;
                Debug.Log(UIManager.daysSurvived);
                Debug.Log("message popping from Sleep Sript, delete after");
                //need to setup sleeping animation
                //need to fix going dark screen
                //need to fix delay so you cant sleep constantly
                //PlayerAnim.SetBool("Sleeping", true);

            }
        }
    }
    public void OpenPanel()
    {
        interactPanel.SetActive(true);
    }
    public void ClosePanel()
    {
        interactPanel.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        OpenPanel();
    }
    private void OnTriggerExit(Collider other)
    {
        ClosePanel();
    }
}
