using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : MonoBehaviour
{
    public Animator PlayerAnim;
    public UIManager UIManager;
    public TimeController Timecontroller;
    public GameObject interactPanel,abilityBar,sleepMenu;
    private AudioSource snoringSound,dayChangedSound;
    [SerializeField]
    private AudioClip clip,dayChangeClip;
    [SerializeField]
    private GameObject daysbar;
    private UIManager uiManager;
    private TimeController timez;
    public bool canmove = true;

    private void Awake()
    {
        Timecontroller = GameObject.FindObjectOfType<TimeController>();
        snoringSound = GetComponent<AudioSource>();
        dayChangedSound = GetComponent<AudioSource>();
        uiManager = GameObject.FindObjectOfType<UIManager>();
        timez = GameObject.FindObjectOfType<TimeController>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                StartCoroutine(SleepyTime(6));
                StartCoroutine(wait(6));
                StartCoroutine(TimeMessageDisplay(6));
                Timecontroller.UpdateTime();
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
        timez.daysSurvived++;
        UIManager.currentDay++;
        UIManager.changeDaysSurvived++;
        abilityBar.SetActive(false);
        sleepMenu.SetActive(true);
        snoringSound.PlayOneShot(clip);
        canmove = false;
        yield return new WaitForSeconds(time);
        canmove = true;
        snoringSound.Stop();
        sleepMenu.SetActive(false);
    }
    IEnumerator TimeMessageDisplay(float time)
    {
        uiManager.UpdateTimeVisual();
        yield return new WaitForSeconds(time);
        daysbar.SetActive(true);
        dayChangedSound.PlayOneShot(dayChangeClip);
        yield return new WaitForSeconds(time);
        dayChangedSound.Stop();
        daysbar.SetActive(false);
    }
    IEnumerator wait(float time)
    {
        yield return new WaitForSeconds(6);
    }
}
