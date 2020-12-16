using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Similar behaviour than walking, but slowly
/// </summary>
public class StateSad : State
{
    private Vector3 _destination;

    public StateSad(StateMachine stateMachine) : base(stateMachine)
    {
        _stateMachine.animal.SetSlowSpeed();
        MoveToDestination();
    }
    
    private void MoveToDestination()
    {
        
        Vector2 randomDestination = Random.insideUnitCircle * 10;
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
        State res;
        if (_stateMachine.animal.isHungry())
        {
            _stateMachine.animal.SetNormalSpeed();
            res = new StateHungry(_stateMachine);
        }
        else if (_stateMachine.animal.isSad())
        {
            res = this;
        }
        else
        {
            _stateMachine.animal.SetNormalSpeed();
            res = new StateWalking(_stateMachine);
        }
        return res;
    }
}
