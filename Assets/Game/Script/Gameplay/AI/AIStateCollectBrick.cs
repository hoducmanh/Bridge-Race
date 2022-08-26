using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateCollectBrick : AIState
{
    private Vector3 targetPos;
    private bool isNeedToCollectBrick;
    private bool isNeedToPlaceBrick;
    private int numOfBrickToCollect = 0;
    private Vector3 tmp;
    public AIStateId GetId()
    {
        return AIStateId.collectBrick;
    }
    public void Enter(AIAgent agent)
    {

        Debug.LogWarning("collect brick");
        isNeedToCollectBrick = true;
        isNeedToPlaceBrick = false;
        agent.Anim.SetFloat(Value.CURRENT_ANIM_VELOCITY, 1f);
        numOfBrickToCollect = Random.Range(5, 8);
    }
    public void Update(AIAgent agent)
    {
        //Debug.Log(agent.enemyRef.navAgent.velocity.sqrMagnitude);
        if (agent.enemyRef.navAgent.velocity.sqrMagnitude < 0.01f|| isNeedToCollectBrick)
        {
            LocateBrick(agent);
            //Debug.Log(targetPos);
            Debug.Log("brick:" + targetPos);
            agent.NavAgent.destination = targetPos;
            tmp = targetPos;
            
        }
        if (agent.enemyRef.brick.Count >= numOfBrickToCollect)
        {
            //Debug.Log("build");
            isNeedToCollectBrick = false;
            agent.StateMachine.ChangeState(AIStateId.buildBridge);
        }
        if((targetPos-tmp).sqrMagnitude <= 0.001f)
        {
            LocateBrick(agent);
        }

    }
    public void Exit(AIAgent agent)
    {

    }
    private void LocateBrick(AIAgent agent)
    {
        Collider[] col = Physics.OverlapSphere(agent.BotTrans.position, 8f, agent.BrickLayerMask);
        if(col.Length > 0)
        {
            int ran = Random.Range(0, col.Length);
            //Debug.Log(targetPos);
            targetPos = col[ran].transform.position;
            isNeedToCollectBrick = false;
        }
    }
}
