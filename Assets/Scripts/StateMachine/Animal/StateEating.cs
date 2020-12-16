using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEating : State
{

    private bool _ate;
    private GameObject _food;
    
    public StateEating(StateMachine stateMachine) : base(stateMachine)
    {
        _ate = false;
        _food = _stateMachine.animal.foundFood.gameObject;
        _stateMachine.animal.MoveToDestination(_food.transform.position);
    }

    public override void Action()
    {
        if (Vector3.Distance(_food.transform.position, _stateMachine.animal.transform.position) < 3)
        {
            _stateMachine.animal.animal.EatFood(_food.GetComponent<ItemBody>().item as Food);
            GameObject.Destroy(_food);
            _ate = true;
        }
    }

    public override State Next()
    {
        State res = this;
        if (_ate)
        {
            res = new StateWalking(_stateMachine);
        }
        return res;
    }
}
