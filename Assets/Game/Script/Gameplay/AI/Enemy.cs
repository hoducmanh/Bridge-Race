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

    protected override void Start()
    {
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
    }

}
