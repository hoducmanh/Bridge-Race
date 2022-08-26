using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AIStateId
{
    collectBrick, buildBridge, fall, idle
}
public interface AIState 
{
    AIStateId GetId();
    void Enter(AIAgent agent);
    void Update(AIAgent agent);
    void Exit(AIAgent agent);
}
