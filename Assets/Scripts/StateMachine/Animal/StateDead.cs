using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateDead : State
{
    public StateDead(StateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine.animal.MoveToDestination(_stateMachine.animal.transform.position);
        _stateMachine.animal.Death();
    }

    public override void Action()
    {
        
    }

    public override State Next()
    {
        return this;
    }
}
