using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RareItem : Item
{
    private int _maxHappinessBonus;
    
    private Vector3 _wornPosition;
    private Vector3 _wornRotation;

    public Vector3 wornPosition => _wornPosition;

    public Vector3 wornRotation => _wornRotation;

    public RareItem(string name, string prefab, int maxHappinessBonus, Vector3 wornPosition, Vector3 wornRotation) : base(name,prefab)
    {
        _maxHappinessBonus = maxHappinessBonus;
        _wornPosition = wornPosition;
        _wornRotation = wornRotation;

    }

}
