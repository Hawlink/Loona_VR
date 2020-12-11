using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    private int _hungerRestoration;

    public Food(string name, string prefab, int hungerRestoration) : base(name, prefab)
    {
        _hungerRestoration = hungerRestoration;
    }
}
