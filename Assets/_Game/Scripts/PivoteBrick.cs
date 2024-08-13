using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PivoteBrick : MonoBehaviour
{
    [SerializeField] GameObject brick;
    [SerializeField] private bool active;

    public void RemoveBrick()
    {
        brick.SetActive(false);
        active = false;
    }

    public void AddBrick()
    {
        brick.SetActive(true);
        active = true;
    }

    public bool IsActive()
    {
        return active;
    }
}
