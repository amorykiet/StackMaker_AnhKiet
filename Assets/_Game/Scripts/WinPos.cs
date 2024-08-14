using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPos : MonoBehaviour
{
    [SerializeField] GameObject ChestClose;
    [SerializeField] GameObject ChestOpen;

    private void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
    }

    public void OpenChest()
    {
        ChestClose.SetActive(false);
        ChestOpen.SetActive(true);
    }
}
