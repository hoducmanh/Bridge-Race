using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateCollectBrick : AIState
{
    private Vector3 targetPos;
    private bool isNeedToCollectBrick;
    private bool isNeedToPlaceBrick;
    public AIStateId GetId()
    {
        return AIStateId.collectBrick;
    }
    public void Enter(AIAgent agent)
    {
        isNeedToCollectBrick = true;
        isNeedToPlaceBrick = false;
        agent.anim.SetFloat(Value.CURRENT_ANIM_VELOCITY, 1f);
    }
    public void Update(AIAgent agent)
    {

    }
    public void Exit(AIAgent agent)
    {

    }
}
