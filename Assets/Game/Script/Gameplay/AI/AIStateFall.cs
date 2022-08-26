using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateFall : AIState
{
    public AIStateId GetId()
    {
        return AIStateId.fall;
    }
    public void Enter(AIAgent agent)
    {
        agent.BotCollider.enabled = false;
        agent.NavAgent.enabled = false;
        agent.Anim.SetTrigger(Value.FALL_ANIM);
    }
    public void Update(AIAgent agent)
    {
        
    }
    public void Exit(AIAgent agent)
    {
        agent.BotCollider.enabled = true;
        agent.NavAgent.enabled = true;
    }
}
