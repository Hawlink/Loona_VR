using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateDrinking : State
{
    private Vector3 _destination;

    private bool _drank;

    public StateDrinking(StateMachine stateMachine) : base(stateMachine)
    {
        _drank = false;
        Vector3 waterPointLocation = GameObject.FindGameObjectWithTag("WaterPoint").transform.position;
        
        _stateMachine.animal.MoveToDestination(waterPointLocation);
        _destination = waterPointLocation;

    }

    IEnumerator Drinking()
    {
        yield return new WaitForSeconds(10f);
        _drank = true;
    }
    
    public override void Action()
    {
        if (Vector3.Distance(_destination, _stateMachine.animal.transform.position) < 2)
        {
            _stateMachine.animal.StartChildCoroutine(Drinking());
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
        else if (_drank)
        {
            res = new StateWalking(_stateMachine);
        }
        return res;
    }
}
