using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine StateMachine;
    public AIStateId InitialState;
    public Animator anim;
    void Start()
    {
        StateMachine = new AIStateMachine(this);
        StateMachine.ChangeState(InitialState);
    }

    void Update()
    {
        StateMachine.Update();
    }
}
