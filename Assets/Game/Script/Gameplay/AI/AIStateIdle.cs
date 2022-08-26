using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateIdle : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.idle;
    }
    public void Enter(AIAgent agent)
    {
        agent.NavAgent.enabled = false;
        agent.Anim.SetFloat(Value.CURRENT_ANIM_VELOCITY, 0);
    }
    public void Update(AIAgent agent)
    {

    }
    public void Exit(AIAgent agent)
    {
        agent.NavAgent.enabled = true;
    }
}
