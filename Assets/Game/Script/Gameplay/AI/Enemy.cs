using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : PlayerCollectBrick
{
    public AIAgent agent;
    public NavMeshAgent navAgent;
    public Transform targetTrans;
    public Stage CurrStage;
    public Collider Col;
    //private bool canMove;
    public Animator animator;

    protected override void Start()
    {
        CurrStage = Stage.One;
        base.Start();
        GameManager.OnGameStateChanged += GameManagerOnGameStateChanged;
    }
    private void GameManagerOnGameStateChanged(GameManager.GameState state)
    {
        switch (state)
        {
            case GameManager.GameState.Playing:
                agent.StateMachine.ChangeState(AIStateId.collectBrick);
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(color[Tag]))
        {
            CollectBrick(other.gameObject);
        }
        if (other.CompareTag(Value.SECOND_FLOOR))
        {
            CurrStage = Stage.Two;
            LevelManager.Instance.SpawnerSecondFloor.SpawnOnSecondFloor(LevelManager.Instance.SecondSpawnerPos.position, 60);
        }
        if (other.CompareTag(Value.THIRD_FLOOR))
        {
            CurrStage = Stage.Three;
            LevelManager.Instance.SpawnerThirdFloor.SpawnOnThirdFloor(LevelManager.Instance.ThirdSpawnerPos.position, 90);
        }
        if (other.CompareTag(Value.PLAYER))
        {
            if (brick.Count < other.GetComponent<PlayerCollectBrick>().brick.Count)
            {
                if (!isOnBridge)
                {
                    TriggerFall();
                    DropAllBrick();
                }
            }
            //if (agent.enemyRef.brick.Count < other.GetComponent<Enemy>().brick.Count)
            //{
            //    TriggerFall();
            //}
        }
    }
    private void TriggerFall()
    {
        StartCoroutine(Fall());
    }
    private IEnumerator Fall()
    {
        agent.StateMachine.ChangeState(AIStateId.fall);
        yield return new WaitForSeconds(5f);
        agent.StateMachine.ChangeState(agent.StateMachine.prevState);
    }

}
