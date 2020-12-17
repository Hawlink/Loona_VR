using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialStateFinished : State
{

    public TutorialStateFinished(StateMachine stateMachine) : base(stateMachine)
    {
        Game game = GameObject.FindObjectOfType<Game>();
        PlayerUtils.MessageCoroutine("Le tutoriel est désormais terminé !", 5, game);
        game.tutoArrow.SetActive(false);
    }

    public override void Action()
    {
        
    }

    public override State Next()
    {
        return this;
    }
}
