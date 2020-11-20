using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Item
{
    private int _hungerRestoration;

    public Food(string name, int hungerRestoration) : base(name)
    {
        _hungerRestoration = hungerRestoration;
    }
}
