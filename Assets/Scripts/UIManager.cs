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

    public Sprite pickaxeSprite;
    public Sprite axeSprite;
    public Sprite swordSprite;
    public Sprite fistsSprite;
    public Sprite healthPotionSprite;

    public InventorySO playerInventory;
    public GameObject inventoryUI;
    [SerializeField]
    private GameObject deathMenu,pauseMenu;
    [SerializeField]
    private Button restartGameBtn;
    bool inventoryUIEnabled = false;

    [SerializeField]
    private TextMeshProUGUI deathDaysSurvivedText,daysSurvivedText, pauseText;

    //private Text daysSurvivedText, pauseText;
    public int daysSurvived;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        hotkeyManager = new HotkeyManager(playerWeapons);
        hotkeyBar.SetHotkeyBar(hotkeyManager);
        daysSurvivedText.text = " Days Survived" + daysSurvived;
        deathDaysSurvivedText.text = " Days Survived" + daysSurvived;
        StartCoroutine(CountTime());
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

    IEnumerator CountTime()
    {
        //change delay to adjust proper time
        yield return new WaitForSeconds(6.0f);
        daysSurvived++;
        daysSurvivedText.text = daysSurvived + " days";
        deathDaysSurvivedText.text = daysSurvived + " days";
        StartCoroutine(CountTime());
    }
    public void OnReloadSceneButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Game Scene");
        playerInventory.InventoryContainer.Clear();
    }
    /*public void LoadMenu()
    {
        //menuUI.SetActive(true);
    }
    public void QuitMenu()
    {
        //menuUI.SetActive(false);
        pauseMenu.SetActive(false);
        deathMenu.SetActive(false);
    }*/
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    public void PauseButton()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
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


