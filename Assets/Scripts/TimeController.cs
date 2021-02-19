using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class TimeController : MonoBehaviour
{
    public Light sun;
    [SerializeField] private float secondsInFullDay = 50f;

    //public event EventHandler OnDayChanged;
    private UIManager uiManager;
    [Range(0, 1)] [SerializeField] private float currentTimeOfDay = 0.5f;
    private float timeMultiplier = 1f;
    private float sunInitialIntesity;
    private int daysSurvived,startingDaysSurvived;

    private void Awake()
    {
        uiManager = GameObject.FindObjectOfType<UIManager>();
    }
    private void Start()
    {
        startingDaysSurvived = daysSurvived;
        sunInitialIntesity = sun.intensity;
    }

    public void Update()
    {
        UpdateSun();

        //currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;

        if(currentTimeOfDay >= 1)
        {
            currentTimeOfDay = 0;
            daysSurvived++;
            if (daysSurvived != startingDaysSurvived)
            {
                //UIManager.currentDay = daysSurvived;
                UIManager.currentDay = daysSurvived;
                uiManager.UpdateTimeVisual();
                //UIManager.UpdateTimeVisual();
                startingDaysSurvived++;
            }
            //OnDayChanged?.Invoke(UpdateTime(), EventArgs.Empty);
        }
        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;
    }
    public int GetDaysSurvived()
    {
        return daysSurvived;
    }

    public void UpdateTime()
    {
        currentTimeOfDay = 1.0f;
    }

    void UpdateSun()
    {
        sun.transform.localRotation = Quaternion.Euler((currentTimeOfDay * 360f) - 90f, 170f, 0f);
        float intesityMultiplier = 1f;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intesityMultiplier = 0.0f;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intesityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intesityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntesity * intesityMultiplier;
    }

}
