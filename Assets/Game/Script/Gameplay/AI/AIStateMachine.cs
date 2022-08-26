using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine 
{
    public AIState[] states;
    public AIAgent agent;
    public AIStateId currState;
    public AIStateId prevState;
    public AIStateMachine(AIAgent agent)
    {
        this.agent = agent;
        int numState = System.Enum.GetNames(typeof(AIStateId)).Length;
        states = new AIState[numState];
    }

    public void RegisterState(AIState state)
    {
        int index = (int)state.GetId();
        states[index] = state;
    }
    public AIState GetState(AIStateId stateId)
    {
        int index = (int)stateId;
        return states[index];
    }
    public void Update()
    {
        GetState(currState)?.Update(agent);
        
    }
    public void ChangeState(AIStateId newState)
    {
        GetState(currState)?.Exit(agent);
        prevState = currState;
        currState = newState;
        GetState(currState)?.Enter(agent);
    }
}


