using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float inputX;
    private float inputZ;
    private Vector3 v_movement;
    private float speed = 2f;
    private CharacterController _charController;
    private Animator _animator;
    private Transform meshPlayer;
    public bool canMoveForward;
    private bool canMove;

    void Start()
    {
        GameObject tempPlayer = GameObject.FindGameObjectWithTag(Value.PLAYER);
        meshPlayer = tempPlayer.GetComponent<Transform>();
        _charController = tempPlayer.GetComponent<CharacterController>();
        _animator = meshPlayer.GetComponent<Animator>();
        canMoveForward = true;
        canMove = true;
    }

    private void FixedUpdate()
    {
        if(canMove)
            HandleWithInput();
    }

    private void Move()
    {
        _charController.Move (v_movement * Time.deltaTime);
        Vector3 lookDir = new Vector3 (v_movement.x, 0, v_movement.z);
        meshPlayer.rotation = Quaternion.LookRotation (lookDir);    
    }

    private void HandleWithInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputZ = Input.GetAxisRaw("Vertical");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Value.BLUE_BRICK))
        {
            PlayerCollectBrick.Instance.CollectBrick(other.gameObject);
        }
        if (other.CompareTag(Value.WIN_POS))
        {
            _animator.SetTrigger(Value.DANCE_ANIM);
            canMove = false;   
        }
        if (other.CompareTag(Value.SECOND_FLOOR))
        {
            LevelManager.Instance.SpawnerSecondFloor.SpawnOnSecondFloor(LevelManager.Instance.SecondSpawnerPos.position, 60);
        }
        if (other.CompareTag(Value.THIRD_FLOOR))
        {
            LevelManager.Instance.SpawnerThirdFloor.SpawnOnThirdFloor(LevelManager.Instance.ThirdSpawnerPos.position, 90);
        }
    }
}
