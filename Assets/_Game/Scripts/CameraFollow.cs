using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float speed;

    private bool following;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!following) return;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.fixedDeltaTime * speed);
    }

    public void SetFollow(bool follow)
    {
        following = follow;
        if (follow)
        {
            target = FindObjectOfType<Player>().transform;
        }
        else
        {
            target = transform;
        }

    }

}