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
    private float hpLossInterval = 0.05f;
    private float hpCurrentAmount;
    private float foodCurrentAmount;
    private float waterCurrentAmount;
    private float foodCurrentAmountPercentage;
    private float waterCurrentAmountPercentage;
    private float hpCurrentAmountPercentage;
    [SerializeField]
    private Outline UIoutline;
    private Player player;
    Animator PlayerAnim;

    private void Awake()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    private void Start()
    {
        PlayerAnim = GetComponent<Animator>();
        hpCurrentAmount = player.PlayerLife;
        hpCurrentAmountPercentage = hpCurrentAmount / hpMaximum;
        foodCurrentAmount = maximum;
        waterCurrentAmount = maximum;
        UIoutline.enabled = false;
        //alternate.transform.Find("StarvingIcon").gameObject.SetActive(true);
        //emptyFood.transform.Find("StarvingIcon").gameObject.SetActive(true);
        //emptyWater.transform.Find("DehydrationIcon").gameObject.SetActive(true);
        this.transform.GetChild(0).gameObject.SetActive(false);

    }
    private void Update()
    {
        if (foodCurrentAmount > minimum)
        {
            foodCurrentAmount -= foodInterval * Time.deltaTime;
            foodCurrentAmountPercentage = foodCurrentAmount / maximum;
            emptyFood.SetActive(false);
        }
        else
        {
            emptyFood.SetActive(true);
            hpCurrentAmount -= hpLossInterval * Time.deltaTime;
            hpCurrentAmountPercentage = hpCurrentAmount / hpMaximum;
        }

        if (waterCurrentAmount > minimum)
        {
            waterCurrentAmount -= waterInterval * Time.deltaTime;
            waterCurrentAmountPercentage = waterCurrentAmount / maximum;
            emptyWater.SetActive(false);
        }
        else
        {
            emptyWater.SetActive(true);
            hpCurrentAmount -= hpLossInterval * Time.deltaTime;
            hpCurrentAmountPercentage = hpCurrentAmount / hpMaximum;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //transform.LookAt(hitObject.transform);
            player.PlayerAnim.SetBool("IsGathering", true);
            Debug.Log("+20");
            foodCurrentAmount += mushroom.HealValue;
            Debug.Log(foodCurrentAmount);
            //put a delay for the animator cause it stops immediately, need to convert scripts cause its total mess
            //no proximity check atm, there is one in the player script. doesn't update hunger value though
            player.PlayerAnim.SetBool("IsGathering", false);

            //foodfillText.text = (foodPercentage + "%");
        }
        /*else
        {
            PlayerAnim.SetBool("IsGathering", false);
        }*/

        foodPercentage = ((int)(foodCurrentAmountPercentage * 100f));
        waterPercentage = ((int)(waterCurrentAmountPercentage * 100f));
        hpPercentage = ((int)(hpCurrentAmountPercentage * 100f));
        hpText.text= (hpPercentage + "%");
        foodfillText.text = (foodPercentage + "%");
        foodfill.fillAmount = foodCurrentAmountPercentage;
        waterfillText.text = (waterPercentage + "%");
        waterfill.fillAmount = waterCurrentAmountPercentage;

        player.StatusSystem(foodCurrentAmount, waterCurrentAmount,hpCurrentAmount);
    }

}
