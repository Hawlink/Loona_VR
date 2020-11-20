using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal
{
    private string _name;
    private int _hunger;
    private int _happiness;
    private int _maxHappiness;
    private List<RareItem> _wear = new List<RareItem>();
    private Food _eat;

    public Animal(string name, int maxHappiness, Food eat)
    {
        _name = name;
        _hunger = 100;
        _maxHappiness = maxHappiness;
        _happiness = _maxHappiness;
        _eat = eat;
    }
    
}
