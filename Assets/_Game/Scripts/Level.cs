using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static Player;

public class Level : MonoBehaviour
{
    [SerializeField] public Transform startPos;
    
    public Player player;

    private int score;
    private bool completed = false;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        score = 0;
        completed = false;
    }

    public void Complete()
    {
        completed = true;
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public bool IsComplete()
    {
        return completed;
    }

}
