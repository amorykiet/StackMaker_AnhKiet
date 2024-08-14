using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private enum GameState {Main, Playing}
    
    private GameState State;

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
                //Something in Main
                break;
            case GameState.Playing:
                //Something in Playing
                break;
        }
    }

    public void OnInit()
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
