using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStateMachine : StateMachine
{

    public TutorialStateMachine() : base(null)
    {
        _current = new TutorialStateMoveHere(this,GameObject.Find("TutoPassage1").transform.position, "Déplacez vous jusqu'à la flèche", false);
    }
    
}
