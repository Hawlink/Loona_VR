using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
   protected State _current;

   private AnimalBody _animal;

   private bool _isDead;

   public AnimalBody animal => _animal;
   public State current => _current;

   public StateMachine(AnimalBody animal)
   {
      _animal = animal;
      _isDead = false;
   }

   public void Action()
   {
      if (_current != null)
      {
         _current.Action();
         _current = _current.Next();
         
         //Generic transitions
         if (_animal != null && !_isDead &&  (_animal.animal.happiness == 0 || _animal.animal.hunger == 100))
         {
            _isDead = true;
            _current = new StateDead(this);
         }
         
      }
   }

   public override string ToString()
   {
      return _current.ToString();
   }
}
