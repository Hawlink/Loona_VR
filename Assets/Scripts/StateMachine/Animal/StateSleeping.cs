using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSleeping : State
{

    private bool _hasSleeped;
    
    public StateSleeping(StateMachine stateMachine) : base(stateMachine)
    {
        _hasSleeped = false;
        _stateMachine.animal.StartChildCoroutine(Sleeping());
    }

    IEnumerator Sleeping()
    {
        yield return new WaitForSeconds(15f);
        _hasSleeped = true;
    }
    
    public override void Action()
    {
        
    }

    public override State Next()
    {
        State res = this;

        if(_hasSleeped)
            res = new StateWalking(_stateMachine);

        return res;
    }
}
