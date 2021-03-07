using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private PlayerWeapons playerWeapons;
    [SerializeField] private UIHotkeyBar hotkeyBar;
    private HotkeyManager hotkeyManager;
    private TimeController timeController;
    public Sprite pickaxeSprite;
    public Sprite axeSprite;
    public Sprite swordSprite;
    public Sprite fistsSprite;
    public Sprite healthPotionSprite;
    public InventorySO playerInventory;
    public GameObject inventoryUI;
    [SerializeField]
    private GameObject deathMenu,pauseMenu,abilitybar;
    [SerializeField]
    private Button restartGameBtn;
    bool inventoryUIEnabled = false;
    [SerializeField]
    private TextMeshProUGUI deathDaysSurvivedText,daysSurvivedText, pauseText;
    public static int currentDay;
    private soundManager ambience;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        hotkeyManager = new HotkeyManager(playerWeapons);
        hotkeyBar.SetHotkeyBar(hotkeyManager);
        ambience = FindObjectOfType<soundManager>();
        UpdateTimeVisual();
    }

    public void UpdateTimeVisual()
    {
        daysSurvivedText.text = " Days Survived " + currentDay;
        deathDaysSurvivedText.text = " Days Survived " + currentDay;
    }

    public void Update()
    {
        hotkeyManager.Update();
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Game Scene");
        playerInventory.InventoryContainer.Clear();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        abilitybar.SetActive(true);
        ambience.ChangeClip();
    }
    public void PauseButton()
    {
        ambience.ChangeClip();
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        abilitybar.SetActive(false);
        restartGameBtn.onClick.RemoveAllListeners();
        restartGameBtn.onClick.AddListener(() => OnReloadSceneButton());
    }
    private void OnEnable()
    {
        //for future utility
        //PlayerDied.endGame += PlayerDiedEndGame;
    }
    private void OnDisable()
    {
        //PlayerDied.endGame -= PlayerDiedEndGame;
    }
    public void PlayerDiedEndGame()
    {
        pauseText.text = "You Died";
        deathMenu.SetActive(true);
        //restarting the game on click if pause is true
        restartGameBtn.onClick.RemoveAllListeners();
        restartGameBtn.onClick.AddListener(() => OnReloadSceneButton());
        Time.timeScale = 0f;
    }
 
}


