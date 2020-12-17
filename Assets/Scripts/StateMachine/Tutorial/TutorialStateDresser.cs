using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStateDresser : State
{
    private Vector3 _dresserLocation;
    private Game _game;
    public TutorialStateDresser(StateMachine stateMachine) : base(stateMachine)
    {
        _dresserLocation = GameObject.FindObjectOfType<Dresser>().transform.position;
        
        _game = GameObject.FindObjectOfType<Game>();
        _game.tutoArrow.SetActive(true);
        _game.tutoArrow.transform.position = _dresserLocation + new Vector3(0, 2, 0);
        _game.messageCanvas.GetComponentInChildren<Text>().text = "Dirigez vous vers l'armoire";

    }

    public override void Action()
    {
        
    }

    public override State Next()
    {
        State res = this;
        if (Vector3.Distance(_game.transform.position, _dresserLocation) < 1.5)
        {
            res = new TutorialStateFinished(_stateMachine);
        }
        return this;
    }
}
