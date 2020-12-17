using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalStateMachine : StateMachine
{
    public AnimalStateMachine(AnimalBody animal) : base(animal)
    {
        _current = new StateWalking(this);
    }
}
