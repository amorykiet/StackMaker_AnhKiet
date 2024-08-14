using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private  TMP_Text ScoreText;
    [SerializeField] private GameObject Victory;
    [SerializeField] private GameObject MainMenu;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMainMenu()
    {
        MainMenu.SetActive(true);
    }

    public void HideMainMenu()
    {
        MainMenu.SetActive(false);
    }

    public void ShowVictory(int score)
    {
        ScoreText.text = score.ToString();
        Victory.SetActive(true);
    }

    public void HideVictory()
    {
        Victory.SetActive(false);
    }
}
