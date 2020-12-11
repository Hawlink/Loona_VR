using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RareItem : Item
{
    private int _maxHappinessBonus;
    
    public RareItem(string name, string prefab, int maxHappinessBonus) : base(name,prefab)
    {
        _maxHappinessBonus = maxHappinessBonus;
    }

}
