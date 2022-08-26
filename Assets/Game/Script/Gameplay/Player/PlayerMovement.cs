using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    public Collider Col;
    private float inputX;
    private float inputZ;
    private Vector3 v_movement;
    private float speed = 2f;
    public Transform PlayerTrans;
    public Animator _animator;
    private Transform meshPlayer;
    public bool canMoveForward;
    private bool canMove;
    //public bool IsOnBridge;
    public PlayerCollectBrick PlayerCol;

    void Start()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag(Value.PLAYER);
        meshPlayer = tempPlayer.GetComponent<Transform>();
        //_charController = tempPlayer.GetComponent<CharacterController>();
        _animator = meshPlayer.GetComponent<Animator>();
        canMoveForward = true;
        canMove = true;
    }

    private void FixedUpdate()
    {
        //Debug.Log(canMove);
        if(canMove)
            HandleWithInput();
    }

    private void Move()
    {
        //_charController.Move (v_movement * Time.deltaTime);
        PlayerTrans.position = Vector3.MoveTowards(PlayerTrans.position, PlayerTrans.position + v_movement, Time.deltaTime * speed);
        Vector3 lookDir = new Vector3 (v_movement.x, 0, v_movement.z);
        meshPlayer.rotation = Quaternion.LookRotation (lookDir);    
    }

    private void HandleWithInput()
    {
        joystickInput();
        v_movement = new Vector3(inputX * speed, 0, inputZ * speed);
        if (v_movement.sqrMagnitude <= 0.01f)
        {
            _animator.SetFloat(Value.CURRENT_ANIM_VELOCITY, 0);
        }
        else
        {
            if (canMoveForward)
            {
                _animator.SetFloat(Value.CURRENT_ANIM_VELOCITY, 1);
                Move();
            }
            else
            {
                if (v_movement.z > 0)
                {
                    _animator.SetFloat(Value.CURRENT_ANIM_VELOCITY, 1);
                    v_movement.z = 0;
                    Move();
                }
                else
                {
                    _animator.SetFloat(Value.CURRENT_ANIM_VELOCITY, 1);
                    canMoveForward = true;
                    Move();
                }
            }
        }
    }
    private void joystickInput()
    {
        inputX = joystick.Horizontal;
        inputZ = joystick.Vertical;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Value.BLUE_BRICK)||other.CompareTag(Value.GREEN_BRICK)||other.CompareTag(Value.RED_BRICK))
        {
            PlayerCol.CollectBrick(other.gameObject);
        }
        if (other.CompareTag(Value.WIN_POS))
        {
            canMove = false;
            _animator.SetTrigger(Value.DANCE_ANIM);
            PlayerCol.RemoveAllBrick();
        }
        if (other.CompareTag(Value.SECOND_FLOOR))
        {
            LevelManager.Instance.SpawnerSecondFloor.SpawnOnSecondFloor(LevelManager.Instance.SecondSpawnerPos.position, 60);
        }
        if (other.CompareTag(Value.THIRD_FLOOR))
        {
            LevelManager.Instance.SpawnerThirdFloor.SpawnOnThirdFloor(LevelManager.Instance.ThirdSpawnerPos.position, 90);
        }
        if (other.CompareTag(Value.PLAYER))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (!PlayerCol.isOnBridge)
            {
                if (PlayerCol.brick.Count < enemy.brick.Count)
                {
                    TriggerFall();
                    PlayerCol.DropAllBrick();
                }
            }
        }
    }
    protected void TriggerFall()
    {
        StartCoroutine(Fall());
    }
    private IEnumerator Fall()
    {
        Col.enabled = false;
        canMove = false;
        _animator.SetTrigger(Value.FALL_ANIM);
        yield return new WaitForSeconds(5f);
        Col.enabled = true;
        canMove = true;
    }
    public void RestrictForwardMovement()
    {
        canMoveForward = false;
    }
}
