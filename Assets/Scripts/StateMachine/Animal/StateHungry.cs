using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// If the animal is hungry, he will go to the player if he is close enough
/// </summary>
public class StateHungry : State
{

    private Game _game;
    
    public StateHungry(StateMachine stateMachine) : base(stateMachine)
    {
        _game = GameObject.FindObjectOfType<Game>();
    }

    public override void Action()
    {
        Vector3 playerPosition = _game.transform.position;
        if (Vector3.Distance(playerPosition, _stateMachine.animal.InitialPosition) < 30)
        {
            _stateMachine.animal.MoveToDestination(playerPosition);
        }
    }

    public override State Next()
    {
        State res;
        if (_stateMachine.animal.foundFood)
        {
            res = new StateEating(_stateMachine);
        }
        else if (_stateMachine.animal.isHungry())
        {
            res = this;
        }
        else if (_stateMachine.animal.isSad())
        {
            res = new StateSad(_stateMachine);
        }
        else
        {
            res = new StateWalking(_stateMachine);
        }
        return res;
    }
}
