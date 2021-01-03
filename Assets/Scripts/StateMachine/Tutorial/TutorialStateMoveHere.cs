using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialStateMoveHere : State
{

    private Vector3 _target;
    private Game _game;
    private bool _secondTeleportation;
        
    public TutorialStateMoveHere(StateMachine stateMachine, Vector3 target, string message, bool secondTeleportation) : base(stateMachine)
    {
        _game = GameObject.FindObjectOfType<Game>();
        _game.messageCanvas.SetActive(true);
        _target = target;
        _game.tutoArrow.SetActive(true);
        _game.tutoArrow.transform.position = _target + new Vector3(0, 5, 0);
        _game.messageCanvas.GetComponentInChildren<Text>().text = message;
        _secondTeleportation = secondTeleportation;
    }

    public override void Action()
    {
        
    }

    public override State Next()
    {
        State res = this;
        if (Vector3.Distance(_target, _game.transform.position) < 6)
        {
            if (!_secondTeleportation)
            {
                res = new TutorialStateCarrot(_stateMachine, GameObject.Find("TutoCarrot").GetComponent<ItemBody>());
            }
            else
            {
                res = new TutorialStateDresser(_stateMachine);
            }
        }
        return res;
    }
}
