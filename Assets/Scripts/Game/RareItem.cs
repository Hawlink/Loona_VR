using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItem : Item
{
    private int _maxHappinessBonus;
    
    public RareItem(string name, int maxHappinessBonus) : base(name)
    {
        _maxHappinessBonus = maxHappinessBonus;
    }

}
