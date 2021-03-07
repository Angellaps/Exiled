using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VitalStats : MonoBehaviour
{
    [SerializeField] FoodSO mushroom;
    public Image foodfill,waterfill;
    public Image empty;
    public GameObject emptyFood;
    public GameObject emptyWater;
    public Text foodfillText, waterfillText, hpText;
    private int foodPercentage, waterPercentage,hpPercentage;
    private float hpMaximum = 100.0f;
    private float maximum = 100.0f;
    private float minimum = 0f;
    private float foodInterval = 0.5f;
    private float waterInterval = 1.0f;
    private float hpLossInterval = 1.0f;
    private float foodCurrentAmount;
    private float waterCurrentAmount;
    private float foodCurrentAmountPercentage;
    private float waterCurrentAmountPercentage;
    private float hpCurrentAmountPercentage;
    public float hpCurrentAmount;
    [SerializeField]
    private Outline UIoutline;
    private Player player;
    Animator PlayerAnim;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
        hpCurrentAmount = 100;
    }
    private void Start()
    {
        PlayerAnim = GetComponent<Animator>();
        foodCurrentAmount = maximum;
        waterCurrentAmount = maximum;
        UIoutline.enabled = false;
        this.transform.GetChild(0).gameObject.SetActive(false);

    }
    private void Update()
    {
        hpCurrentAmount = player.PlayerLife;
        hpCurrentAmountPercentage = player.PlayerLife / hpMaximum;
        if (foodCurrentAmount > minimum)
        {
            foodCurrentAmount -= foodInterval * Time.deltaTime;
            foodCurrentAmountPercentage = foodCurrentAmount / maximum;
            emptyFood.SetActive(false);
        }
        else if (foodCurrentAmount <= minimum)
        {
            emptyFood.SetActive(true);
            hpCurrentAmount -= hpLossInterval * Time.deltaTime;
        }

        if (waterCurrentAmount > minimum)
        {
            waterCurrentAmount -= waterInterval * Time.deltaTime;
            waterCurrentAmountPercentage = waterCurrentAmount / maximum;
            emptyWater.SetActive(false);
        }
        else if (waterCurrentAmount <= minimum)
        {
            emptyWater.SetActive(true);
            hpCurrentAmount -= hpLossInterval * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //transform.LookAt(hitObject.transform);
            player.PlayerAnim.SetBool("IsGathering", true);
            Debug.Log("+20");
            foodCurrentAmount += mushroom.HealValue;
            if (foodCurrentAmount > maximum)
            {
                foodCurrentAmount = maximum;
            }
            Debug.Log(foodCurrentAmount);
            //need put a delay for the animator cause it stops immediately
            //no proximity check atm
            player.PlayerAnim.SetBool("IsGathering", false);
        }

        foodPercentage = ((int)(foodCurrentAmountPercentage * 100f));
        waterPercentage = ((int)(waterCurrentAmountPercentage * 100f));
        hpPercentage = ((int)(hpCurrentAmountPercentage * 100f));
        hpText.text = (hpPercentage + "%");
        foodfillText.text = (foodPercentage + "%");
        foodfill.fillAmount = foodCurrentAmountPercentage;
        waterfillText.text = (waterPercentage + "%");
        waterfill.fillAmount = waterCurrentAmountPercentage;
        Debug.Log("to food" + foodCurrentAmount);
        Debug.Log("to water" + waterCurrentAmount);
        player.StatusSystem(foodCurrentAmount, waterCurrentAmount, hpCurrentAmount);
    }

}
