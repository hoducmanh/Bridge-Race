using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateBuildBridge : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.buildBridge;
    }
    public void Enter(AIAgent agent)
    {
        agent.Anim.SetFloat(Value.CURRENT_ANIM_VELOCITY, 1f);
        if (agent.enemyRef.CurrStage == Stage.Two)
        {
            Debug.LogError("enter this");
            agent.NavAgent.destination = WaypointControl.Instance.secondWaypoint.position;
        }
        else if (agent.enemyRef.CurrStage == Stage.Three)
        {
            agent.NavAgent.destination = WaypointControl.Instance.thirdWaypoint.position;
        }
        else
        {
            //Debug.Log("still here");
            agent.NavAgent.destination = WaypointControl.Instance.firstWaypoint.position;
        }
        
    }
    public void Update(AIAgent agent)
    {
        if(agent.enemyRef.brick.Count <= 0)
        {
            agent.StateMachine.ChangeState(AIStateId.collectBrick);
        }
        if(agent.enemyRef.CurrStage == Stage.Two)
        {
/*            Debug.Log(agent.NavAgent.destination);
*/            agent.NavAgent.SetDestination(WaypointControl.Instance.thirdWaypoint.position);
            Debug.Log("3rd"+WaypointControl.Instance.thirdWaypoint.position);
            
        }
        if (agent.enemyRef.CurrStage == Stage.Three)
        {
            agent.NavAgent.SetDestination(WaypointControl.Instance.thirdWaypoint.position);
        }
    }
    public void Exit(AIAgent agent)
    {
    }
}
