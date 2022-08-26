using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCollectBrick : MonoBehaviour
{
    [SerializeField] private Transform root;
    public Stack<GameObject> brick = new Stack<GameObject>();
    [SerializeField] private Stack<Vector3> brickPos = new Stack<Vector3>();
    [SerializeField] private Transform brickPlacer;
    [SerializeField] private Transform placedBrick;
    [SerializeField] private Transform placedBricks;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject rootOfBrick; //only use for removing all brick
    public LayerMask BrickLayerMask;
    public BrickColor Tag;
    public Dictionary<BrickColor, string> color = new Dictionary<BrickColor, string>();
    public bool isOnBridge;
    private int brickNum = 0;
    private Vector3 bridgePos;
    BrickPooler objPool;
    public PlayerMovement PlayerMove;

    protected virtual void Start()
    {
        objPool = BrickPooler.Instance;
        color.Add(BrickColor.BlueBrick, Value.BLUE_BRICK);
        color.Add(BrickColor.GreenBrick, Value.GREEN_BRICK);
        color.Add(BrickColor.RedBrick, Value.RED_BRICK);    
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
        if (Physics.Raycast(brickPlacer.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, BrickLayerMask))
        {
            Debug.DrawRay(brickPlacer.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            if (hit.collider.CompareTag(Value.BRIDGE))
            {
                isOnBridge = true;
                if (brickNum > 0)
                {
                    PlaceBricks(hit);
                }
                else
                {
                    if(color[Tag] == Value.BLUE_BRICK)
                        PlayerMove.RestrictForwardMovement();
                }
            }
            else if(hit.collider.CompareTag(Value.FIRST_FLOOR)|| hit.collider.CompareTag(Value.SECOND_FLOOR)|| hit.collider.CompareTag(Value.THIRD_FLOOR))
                isOnBridge = false;
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
        DropBrick(color[Tag]);
        brickNum--;
    }
    private void DropBrick(string tag)
    {
        int cnt = brick.Count;
        
        if(cnt > 0)
        {
            GameObject lastBrick = brick.Pop();
            Vector3 lastBrickPos = brickPos.Pop();
            lastBrick.transform.SetParent(null);
            objPool.DespawnToPool(tag, lastBrick);
            objPool.SpawnFromPool(tag, lastBrickPos, Quaternion.identity);
        }
        //rootPos.y -= 0.1f; 
    }
    public void DropAllBrick()
    {
        int cnt = brick.Count;
        //if (cnt > 0)
        //{
        //    for (int i = 0; i < cnt; i++)
        //    {
        //        DropBrick(color[Tag]);
        //    }
        //}
        for (int i = 0; i < cnt; i++)
        {
            DropBrick(color[Tag]);
        }
    }

    public void RemoveAllBrick()
    {
        rootOfBrick.SetActive(false);
    }
    public enum BrickColor
    {
        BlueBrick,
        GreenBrick,
        RedBrick
    }
}
