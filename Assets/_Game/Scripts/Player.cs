using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public static event Action<int> OnPlayerWin;

    [SerializeField] private Animator animator;
    [SerializeField] private float rayOffset = 0.25f;
    [SerializeField] private Transform modelTransform;
    [SerializeField] private GameObject brickPref;

    public float brickSize = 0.3f;
    public float speed = 1f;

    private Stack<GameObject> BrickStack = new Stack<GameObject>();
    private Vector3 mouseStartPoint;
    private Vector3 mouseEndPoint;
    private bool moving = false;
    private Vector3 newPosition;
    private Vector3 direction;
    private PivoteBrick pivoteBrickCurrent;
    private bool winning;
    private int score;



    private void Start()
    {
        OnInit();
    }
    // Update is called once per frame
    private void Update()
    {
        if (winning) return;
        //Get direction
        if (Input.GetMouseButtonDown(0)) 
        {
            mouseStartPoint = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && !moving) 
        { 
            mouseEndPoint = Input.mousePosition;
            Vector3 rawDirection = Vector3.Normalize(mouseEndPoint - mouseStartPoint);

            if (Mathf.Abs(Vector3.Angle(rawDirection, Vector3.up)) < 45f)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                direction = Vector3.forward;

            }
            else if (Mathf.Abs(Vector3.Angle(rawDirection, Vector3.right)) < 45f)
            {
                transform.rotation = Quaternion.Euler(0f, 90f, 0f);
                direction = Vector3.right;
            }
            else if (Mathf.Abs(Vector3.Angle(rawDirection, Vector3.left)) < 45f)
            {
                transform.rotation = Quaternion.Euler(0f, -90f, 0f);
                direction = Vector3.left;
            }
            else if (Mathf.Abs(Vector3.Angle(rawDirection, Vector3.down)) < 45f)
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
                direction = Vector3.back;
            }

            //Start to move
            moving = true;
            Move(ref direction, out newPosition);

        }

        //Moving
        if (moving)
        {
            if(Vector3.Distance(newPosition, transform.position) > 0.0001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * speed);
            }
            else
            {
                Move(ref direction, out newPosition);
            }

        }
        else
        {
            direction = Vector3.zero;
        }

    }

    private void OnInit()
    {
        winning = false;
        newPosition = transform.position;
        direction = Vector3.zero;
        //Anim Ildle (not good!!!)
        animator.SetInteger("renwu", 0);
    }

    private void Move(ref Vector3 direction, out Vector3 newPosition)
    {
        newPosition = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * rayOffset, direction, out hit, 1.3f))
        {
            //Hit Wall
            if (hit.collider.CompareTag("Wall"))
            {
                CollideWithWall(hit);
            }
            //Collect Brick

            if (hit.collider.CompareTag("BrickOnPlatform"))
            {
                CollideWithBrickOnPlatform(hit);
            }
            //On Bridge
            if (hit.collider.tag == "BrickOnBridge")
            {
                CollideWithBrickOnBridge(hit);
            }   
            //Win
            if (hit.collider.tag == "WinPos")
            {
                CollideWithWinPos(hit);
            }
        }

        //On Platform
        else
        {
            newPosition = transform.position + direction;
            return;
        }
    }

    private void CollideWithWall(RaycastHit hit)
    {
        moving = false;
    }

    private void CollideWithBrickOnPlatform(RaycastHit hit)
    {
        PivoteBrick pivoteBrick = hit.collider.GetComponent<PivoteBrick>();
        if (pivoteBrick.IsActive())
        {
            AddBrick();
            pivoteBrick.RemoveBrick();
        }
        newPosition = transform.position + direction;
    }

    private void CollideWithBrickOnBridge(RaycastHit hit)
    {
        PivoteBrick pivoteBrick = hit.collider.GetComponent<PivoteBrick>();

        //Go back
        if (pivoteBrick.IsActive())
        {
            AddBrick();
            pivoteBrick.RemoveBrick();
            newPosition = transform.position + direction;
        }

        //Go toward
        else if (BrickStack.Count > 0)
        {
            if (BrickStack.Count == 1)
            {
                pivoteBrickCurrent = pivoteBrick;
            }
            RemoveBrick();
            pivoteBrick.AddBrick();
            newPosition = transform.position + direction;
        }

        //Return
        else if (BrickStack.Count <= 0)
        {
            direction = -direction;
            if (pivoteBrickCurrent != null)
            {
                AddBrick();
                pivoteBrickCurrent.RemoveBrick();
            }
        }
    }

    private void CollideWithWinPos(RaycastHit hit)
    {
        WinPos winPos = hit.collider.gameObject.GetComponent<WinPos>();
        winPos.OpenChest();
        // winPos.FireFirework();
        moving = false;
        winning = true;
        animator.SetInteger("renwu", 2);
        score = BrickStack.Count;
        ClearBrick();
        Invoke(nameof(EmitEventWin), 2);
    }

    private void AddBrick() 
    {
        var brick = Instantiate<GameObject>(brickPref, transform.position + Vector3.up * brickSize * BrickStack.Count, Quaternion.Euler(-90, 0, 0), transform);
        BrickStack.Push(brick);
        modelTransform.position = modelTransform.position + Vector3.up * brickSize;
        brick.transform.localRotation = Quaternion.Euler(-90, 0, 0);
    }

    private void RemoveBrick() 
    {
        var brick = BrickStack.Pop();
        Destroy(brick);
        modelTransform.position = modelTransform.position + Vector3.down * brickSize;
    }

    private void ClearBrick()
    {
        while(BrickStack.Count > 0)
        {
            RemoveBrick();
        }
    }

    private void EmitEventWin()
    {
        OnPlayerWin?.Invoke(score);
    }
    


}
