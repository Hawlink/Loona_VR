using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateWalking : State
{

    private Vector3 _destination;
    private Game _game;
    
    public StateWalking(StateMachine stateMachine) : base(stateMachine)
    {
        MoveToDestination();
        _game = GameObject.FindObjectOfType<Game>();

    }

    private void MoveToDestination()
    {
        
        Vector2 randomDestination = Random.insideUnitCircle * 30;
        Vector3 animalPosition = _stateMachine.animal.InitialPosition;
        Vector3 destination = new Vector3(animalPosition.x + randomDestination.x, animalPosition.y,
            animalPosition.z + randomDestination.y);
        _stateMachine.animal.MoveToDestination(destination);
        _destination = destination;
    }
    
    public override void Action()
    {
        if (Vector3.Distance(_destination, _stateMachine.animal.NavMeshAgent.transform.position) < 3)
        {
            MoveToDestination();
        }
    }

    public override State Next()
    {
        State res = this;
        
        if (_stateMachine.animal.isHungry())
        {
            res = new StateHungry(_stateMachine);
        }
        else if (_stateMachine.animal.isSad())
        {
            res = new StateSad(_stateMachine);
        }
        else if (Vector3.Distance(_stateMachine.animal.transform.position, _game.transform.position) < 5)
        {
            res = new StateNearPlayer(_stateMachine);
        }
        else if (Random.Range(0, 600) == 1)
        {
            res = new StateSleeping(_stateMachine);
        }
        else if (Random.Range(0, 600) == 5)
        {
            res = new StateDrinking(_stateMachine);
        }
        
        return res;
    }
}
