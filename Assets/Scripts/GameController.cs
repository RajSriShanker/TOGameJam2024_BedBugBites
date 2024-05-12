using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] RectTransform mainMenuPanel;
    [SerializeField] RectTransform levelSelectPanel;
    [SerializeField] RectTransform LevelButtons;

    [SerializeField] Button startButton;
    [SerializeField] Button levelSelectButton;

    [SerializeField] Vector2 enabledLocation = new Vector2(0, 0);
    [SerializeField] Vector2 disabledLocation = new Vector2(-982, 0);

    [SerializeField] float uiMoveSpeed = 0.5f;
    
    public bool isRestarting = false;
    public int restartDelay = 1;

    ChildManager childManager;
    SoundController soundController;

 
    private void Awake()
    {
        int numberOfGameControllers = FindObjectsOfType<GameController>().Length;
        if (numberOfGameControllers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        childManager = GameObject.Find("Child Manager").GetComponent<ChildManager>();
        soundController = GameObject.Find("Sound Manager").GetComponent<SoundController>();

        soundController.PlayBackgroundMusic();

        mainMenuPanel.anchoredPosition = enabledLocation;
        levelSelectPanel.anchoredPosition = disabledLocation;
        startButton.onClick.AddListener(LoadLevel1);
        levelSelectButton.onClick.AddListener(LevelSelect);
    }

    public void LoadLevel1()
    {
        childManager.ClearLists();
        SceneManager.LoadScene("Level1Scene");
        DisableUI();
        EnableLevelUI();
    }

    public void LoadMainMenu()
    {
        childManager.ClearLists();
        SceneManager.LoadScene("MainScene");
        EnableUI();
    }

    public void RestartCalling()
    { 
        StartCoroutine(RestartScene());
    }

    public IEnumerator RestartScene()
    { 
        isRestarting = true;
        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        childManager.ClearLists();
        isRestarting = false;
    }

    public void LevelSelect()
    {
        if (mainMenuPanel.anchoredPosition == enabledLocation)
        {
            mainMenuPanel.DOAnchorPos(disabledLocation, uiMoveSpeed);
            levelSelectPanel.DOAnchorPos(enabledLocation, uiMoveSpeed);
        }
        else
        {
            mainMenuPanel.DOAnchorPos(enabledLocation, uiMoveSpeed);
            levelSelectPanel.DOAnchorPos(disabledLocation, uiMoveSpeed);
        }
    }

    public void LoadScene(string sceneName)
    {
        if (sceneName == "MainScene")
        {
            childManager.ClearLists();
            LoadMainMenu();
            EnableUI();
        }
        else
        {
            SceneManager.LoadScene(sceneName);
            childManager.ClearLists();
            DisableUI();
        }
    }

    public void EnableUI()
    {
        mainMenuPanel.DOAnchorPos(enabledLocation, uiMoveSpeed);
        levelSelectPanel.DOAnchorPos(disabledLocation, uiMoveSpeed);
        LevelButtons.DOAnchorPos(disabledLocation, uiMoveSpeed);
    }

    public void DisableUI()
    {
        mainMenuPanel.DOAnchorPos(disabledLocation, uiMoveSpeed);
        levelSelectPanel.DOAnchorPos(disabledLocation, uiMoveSpeed);
        LevelButtons.DOAnchorPos(disabledLocation, uiMoveSpeed);
    }

    public void EnableLevelUI()
    {
        LevelButtons.DOAnchorPos(enabledLocation, uiMoveSpeed);
    }

}
