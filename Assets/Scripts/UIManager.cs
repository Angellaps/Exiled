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
    //private soundManager soundmanager;

    public static int currentDay;
    private AudioSource ambience;
    //private float backgroundVolume = 1f;
    //private float selectedVolume;
    //[SerializeField]
    //private AudioClip menuClip,gameClip;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        //selectedVolume = backgroundVolume;
        hotkeyManager = new HotkeyManager(playerWeapons);
        hotkeyBar.SetHotkeyBar(hotkeyManager);
        ambience = GetComponent<AudioSource>();
        UpdateTimeVisual();
    }

    public void UpdateTimeVisual()
    {
        daysSurvivedText.text = " Days Survived " + currentDay;
        deathDaysSurvivedText.text = " Days Survived " + currentDay;
    }

    public void Update()
    {
        //ambience.volume = backgroundVolume;
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
        //soundManager.Instance.SetAmbienceVolume()
        //SetAmbienceVolume(selectedVolume);
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        abilitybar.SetActive(true);
        //soundManager.Instance.PlayGameSound();
        //soundManager.Instance.PlayGameSoundx(soundManager.Instance.gameClip);
        soundManager.Instance.PlayGameSoundx(soundManager.Instance.gameClip, soundManager.Instance.menuClip);
        //ambience.Stop();
        //ambience.clip = gameClip;
        //ambience.Play();
    }
    public void PauseButton()
    {
        //keeping the value of the selected backgroundVolue to re-adjust after pause
        //selectedVolume = backgroundVolume;
        //SetAmbienceVolume(0);
        //ambience.Stop();
        //ambience.clip = menuClip;
        //ambience.Play();
        //soundManager.Instance.PlayMenuSound();
        //soundManager.Instance.PlayGameSoundx(soundManager.Instance.menuClip);
        soundManager.Instance.PlayGameSoundx(soundManager.Instance.menuClip, soundManager.Instance.gameClip);
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
    /*public void SetAmbienceVolume(float vol)
    {
        backgroundVolume = vol;
    }*/
 
}


