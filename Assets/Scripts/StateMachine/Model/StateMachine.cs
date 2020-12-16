using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
   protected State _current;

   private AnimalBody _animal;

   public AnimalBody animal => _animal;
   public State current => _current;

   public StateMachine(AnimalBody animal)
   {
      _animal = animal;
      _current = new StateWalking(this);
   }

   public void Action()
   {
      if (_current != null)
      {
         _current.Action();
         _current = _current.Next();
      }
   }

   public override string ToString()
   {
      return _current.ToString();
   }
}
