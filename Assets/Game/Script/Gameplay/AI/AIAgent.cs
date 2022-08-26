using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIAgent : MonoBehaviour
{
    public AIStateMachine StateMachine;
    public AIStateId InitialState;
    public Animator Anim;
    public NavMeshAgent NavAgent;
    public Transform BotTrans;
    public Collider BotCollider;
    public LayerMask BrickLayerMask;
    public Enemy enemyRef;
    public AIStateId currState;
    void Start()
    {
        StateMachine = new AIStateMachine(this);
        StateMachine.RegisterState(new AIStateCollectBrick());
        StateMachine.RegisterState(new AIStateBuildBridge());
        StateMachine.RegisterState(new AIStateFall());
        StateMachine.ChangeState(InitialState);
    }

    void Update()
    {
        StateMachine.Update();
    }
}
