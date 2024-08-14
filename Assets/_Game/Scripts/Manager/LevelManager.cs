using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [SerializeField] private List<Level> Levels = new();
    [SerializeField] private Player player;
    [SerializeField] private CameraFollow cam;

    private List<Level> currentLevelList = new();
    private List<Player> currentPlayerList = new();

    private int currentLevel;
    private void OnEnable()
    {
        Player.OnPlayerWin += CompleteLevel;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        if (PlayerPrefs.HasKey("currentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("currentLevel");
        }
        else
        {
            currentLevel = 0;
        }
    }

    public void CompleteLevel(int score)
    {
        UIManager.Instance.ShowVictory(score);
        if (currentLevel < Levels.Count - 1)
        {
            currentLevel++;
        }
        PlayerPrefs.SetInt("currentLevel", currentLevel);
    }

    public void LoadLevel()
    {
        LoadLevel(currentLevel);
    }

    public void LoadLevel(int levelIndex)
    {

        var level_ = Instantiate(Levels[levelIndex], transform);
        var player_ = Instantiate(player);
        currentLevelList.Add(level_);
        currentPlayerList.Add(player_);
        player_.transform.position = level_.startPos.position;
        player_.transform.rotation = level_.startPos.rotation;
        cam.SetFollow(true);
    }

    public void ReloadLevel()
    {
        ClearLevel();
        LoadLevel(--currentLevel);
    }

    public void ClearLevel()
    {
        foreach (var level in currentLevelList)
        {
            Destroy(level.gameObject);
        }

        foreach (var player in currentPlayerList)
        {
            Destroy(player.gameObject);
        }
        currentLevelList.Clear();
        currentPlayerList.Clear();
        cam.SetFollow(false);
    }

}
