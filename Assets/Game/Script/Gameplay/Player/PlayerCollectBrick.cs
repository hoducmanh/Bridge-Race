using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCollectBrick : Singleton<PlayerCollectBrick>
{
    [SerializeField] private Transform root;
    [SerializeField] private Stack<GameObject> brick = new Stack<GameObject>();
    [SerializeField] private Stack<Vector3> brickPos = new Stack<Vector3>();
    [SerializeField] private Transform brickPlacer;
    [SerializeField] private Transform placedBrick;
    [SerializeField] private Transform placedBricks;
    [SerializeField] private Transform player;

    private int brickNum = 0;
    //private float brickCount;
    private Vector3 bridgePos;
    BrickPooler objPool;
    public PlayerMovement playerMove;

    private void Start()
    {
        objPool = BrickPooler.Instance;
    }
    private void FixedUpdate()
    {
        CheckBridge();
    }
    public void CollectBrick(GameObject newBrick)
    {
        AddBrick(newBrick);
    }

    private void AddBrick(GameObject newBrick)
    {
        brickPos.Push(newBrick.transform.localPosition);
        Transform peakBrick;
        Transform newBricksTrans = newBrick.transform;
        if (brick.Count <= 0)
        {
            peakBrick = root;   
        }
        else
        {
            peakBrick = brick.Peek().transform;
        }
        var newBrickPos = peakBrick.position;
        newBrickPos.y += 0.1f;
        newBricksTrans.position = newBrickPos;
        newBricksTrans.rotation = player.rotation;
        newBricksTrans.SetParent(root);
        brick.Push(newBrick);
        
        brickNum++;
    }

    private void CheckBridge()
    {
        RaycastHit hit;
        if (Physics.Raycast(brickPlacer.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(brickPlacer.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            if (hit.collider.CompareTag(Value.BRIDGE))
            {
                Debug.Log("hit");
                if (brickNum > 0)
                {
                    PlaceBricks(hit);
                }
                else
                {
                    RestrictForwardMovement();
                }
            }

        }
        else
        {
            Debug.DrawRay(brickPlacer.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
        }
    }

    private void PlaceBricks(RaycastHit hit)
    {
        bridgePos = hit.collider.transform.position;
        hit.collider.gameObject.SetActive(false);
        Instantiate(placedBrick, bridgePos, placedBrick.transform.rotation, placedBricks);
        DropBrick(Value.BLUE_BRICK);
        brickNum--;
    }

    private void DropBrick(string tag)
    {
        GameObject lastBrick = brick.Pop();
        Vector3 lastBrickPos = brickPos.Pop();
        lastBrick.transform.SetParent(null);
        objPool.DespawnToPool(tag, lastBrick);
        objPool.SpawnFromPool(tag, lastBrickPos, Quaternion.identity);
        //rootPos.y -= 0.1f; 
    }

    private void RestrictForwardMovement()
    {
        playerMove.canMoveForward = false;
    }
}
