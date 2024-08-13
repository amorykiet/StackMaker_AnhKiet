using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState {Main, Playing}
    public GameState State;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnInit();
    }

    private void Update()
    {
        switch (State)
        {
            case GameState.Main:

                break;
            case GameState.Playing:

                break;
        }
    }

    void OnInit()
    {
        GoMain();
    }

    public void Play()
    {
        State = GameState.Playing;
        UIManager.Instance.HideMainMenu();
        UIManager.Instance.HideVictory();
        LevelManager.Instance.LoadLevel();
    }

    public void GoMain()
    {
        State = GameState.Main;
        UIManager.Instance.ShowMainMenu();
        UIManager.Instance.HideVictory();
        LevelManager.Instance.ClearLevel();
    }
}
