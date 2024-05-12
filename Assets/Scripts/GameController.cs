using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] RectTransform mainMenuPanel;
    [SerializeField] RectTransform levelSelectPanel;

    [SerializeField] Button startButton;
    [SerializeField] Button levelSelectButton;

    [SerializeField] Vector2 enabledLocation = new Vector2(0, 0);
    [SerializeField] Vector2 disabledLocation = new Vector2(-982, 0);

    [SerializeField] float uiMoveSpeed = 0.5f;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        mainMenuPanel.anchoredPosition = enabledLocation;
        levelSelectPanel.anchoredPosition = disabledLocation;
        startButton.onClick.AddListener(StartGame);
        levelSelectButton.onClick.AddListener(LevelSelect);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1Scene");
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
}
