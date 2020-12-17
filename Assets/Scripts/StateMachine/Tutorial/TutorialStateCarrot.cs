using UnityEngine;
using UnityEngine.UI;

public class TutorialStateCarrot : State
{

    private ItemBody _tutoCarrot;
    private Game _game;
    
    public TutorialStateCarrot(StateMachine stateMachine, ItemBody carrot) : base(stateMachine)
    {
        _tutoCarrot = carrot;
        
        _game = GameObject.FindObjectOfType<Game>();
        _game.tutoArrow.SetActive(true);
        _game.tutoArrow.transform.position = carrot.transform.position + new Vector3(0, 5, 0);
        _game.messageCanvas.GetComponentInChildren<Text>().text = "Ramasser la carotte";

    }

    public override void Action()
    {
        
    }

    public override State Next()
    {
        State res = this;
        if (_tutoCarrot == null)
        {
              res = new TutorialStateMoveHere(_stateMachine,GameObject.Find("TutoPassage3").transform.position,"Entrez dans la cabanne",true);                 
        }
        return res;
    }
}
