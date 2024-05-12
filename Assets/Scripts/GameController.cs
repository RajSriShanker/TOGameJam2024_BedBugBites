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

    [SerializeField] Button startButton;
    [SerializeField] Button levelSelectButton;

    [SerializeField] Vector2 enabledLocation = new Vector2(0, 0);
    [SerializeField] Vector2 disabledLocation = new Vector2(-982, 0);

    [SerializeField] float uiMoveSpeed = 0.5f;

    ChildManager childManager;

 
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
    }

    public void LoadMainMenu()
    {
        childManager.ClearLists();
        SceneManager.LoadScene("MainScene");
        EnableUI();
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
    }

    public void DisableUI()
    {
        mainMenuPanel.DOAnchorPos(disabledLocation, uiMoveSpeed);
        levelSelectPanel.DOAnchorPos(disabledLocation, uiMoveSpeed);
    }

}
