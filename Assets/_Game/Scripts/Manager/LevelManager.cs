using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    [SerializeField] private List<Level> Levels = new();
    [SerializeField] Player player;
    [SerializeField] CameraFollow cam;

    List<Level> currentLevelList = new();
    List<Player> currentPlayerList = new();

    int currentLevel;
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
    void OnInit()
    {
        currentLevel = 0;
    }

    public void CompleteLevel(int score)
    {
        UIManager.Instance.ShowVictory(score);
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

    public void ReloadLevel()
    {
        ClearLevel();
        LoadLevel(currentLevel);
    }

    public void NextLevel()
    {
        ClearLevel();
        if (currentLevel < Levels.Count - 1)
        {
            currentLevel++;
        }
        LoadLevel(currentLevel);
    }
}
