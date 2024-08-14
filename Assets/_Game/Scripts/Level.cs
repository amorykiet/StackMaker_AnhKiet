using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using static Player;

public class Level : MonoBehaviour
{
    [SerializeField] public Transform startPos;

    private void Start()
    {
        OnInit();
    }

    public void OnInit() { }
}
