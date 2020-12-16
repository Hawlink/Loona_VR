using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Food : Item
{
    private int _hungerRestoration;

    public int hungerRestoration => _hungerRestoration;

    public Food(string name, string prefab, int hungerRestoration) : base(name, prefab)
    {
        _hungerRestoration = hungerRestoration;
    }
}
