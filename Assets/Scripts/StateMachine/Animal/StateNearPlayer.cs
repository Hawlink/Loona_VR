using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNearPlayer : State
{
    private Game _game;
    public StateNearPlayer(StateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine.animal.NavMeshAgent.SetDestination(_stateMachine.animal.transform.position);
        _game = GameObject.FindObjectOfType<Game>();
    }

    public override void Action()
    {
        
    }

    public override State Next()
    {
        State res = this;
        if (_stateMachine.animal.foundFood && _stateMachine.animal.isHungry())
        {
            res = new StateEating(_stateMachine);
        }
        else if (Vector3.Distance(_stateMachine.animal.transform.position, _game.transform.position) > 4)
        {
            res = new StateWalking(_stateMachine);
        }

        return res;
    }
}
