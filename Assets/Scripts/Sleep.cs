using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    public Animator PlayerAnim;
    public UIManager UIManager;
    public TimeController Timecontroller;
    public GameObject interactPanel,abilityBar,sleepMenu;
    private AudioSource snoringSound;
    [SerializeField]
    private AudioClip clip;
    public bool canmove = true;
    /*private void Start()
    {
        //UIManager = FindObjectOfType<UIManager>();
    }*/

    private void Awake()
    {
        Timecontroller = GameObject.FindObjectOfType<TimeController>();
        snoringSound = GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //Debug.Log(UIManager.daysSurvived);
                abilityBar.SetActive(false);
                sleepMenu.SetActive(true);
                //UIManager.daysSurvived++;
                snoringSound.PlayOneShot(clip);
                StartCoroutine(SleepyTime(6));
                Timecontroller.UpdateTime();
                //Debug.Log(UIManager.daysSurvived);
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

    IEnumerator SleepyTime(float time)
    {
        canmove = false;
        yield return new WaitForSeconds(time);
        canmove = true;
        snoringSound.Stop();
        sleepMenu.SetActive(false);
    }
}
