using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalStats : MonoBehaviour
{
    public Image fill;
    public Image empty;
    public GameObject alternate;
    public Text fillText;
    private int percentage;
    private float maximum = 100.0f;
    private float minimum = 0f;
    private float interval = 10.0f;
    private float currentAmount;
    private float currentAmountPercentage;
    [SerializeField]
    private Outline UIoutline;
    private Player player;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    private void Start()
    {
        currentAmount = maximum;
        UIoutline.enabled = false;
        //alternate.transform.Find("StarvingIcon").gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);

    }
    private void Update()
    {
        if (currentAmount > minimum)
        {
            currentAmount -= interval * Time.deltaTime;
            currentAmountPercentage = currentAmount / maximum;
        }
        
        percentage = ((int)(currentAmountPercentage*100f));
        fillText.text = (percentage+ "%");
        fill.fillAmount = currentAmountPercentage;

        player.StatusSystem(currentAmountPercentage);

        if (percentage< 25 && percentage !=0)
        {
            //UIoutline.enabled = !UIoutline.enabled;
            UIoutline.enabled = true;
        }
        else if (percentage == 0)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            //empty.enabled = false;
            UIoutline.enabled = true;
        }
        else
        {
            UIoutline.enabled = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void addAmount(float value)
    {
        currentAmount += value;
    }
    public void decreaseAmount(float value)
    {
        currentAmount -= value;
    }

}
