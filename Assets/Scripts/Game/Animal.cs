using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Animal
{
    private string _name;
    public string name => _name;
    private int _hunger;
    private int _happiness;
    private int _maxHappiness;
    private RareItem _wear;
    public RareItem wear => _wear;
    private RareItem _canWear;
    private Food _eat;
    public Food eat => _eat;
    
    public RareItem canWear => _canWear;

    public int hunger => _hunger;

    public int happiness => _happiness;

    public void SetProperties(int happiness, int hunger, Food eat, RareItem wear, RareItem canWear)
    {
        _happiness = happiness;
        _hunger = hunger;
        _wear = wear;
        _canWear = canWear;
        _eat = eat;
    }
    
    public Animal(string name, int maxHappiness, Food eat, RareItem canWear)
    {
        _name = name;
        _hunger = 0;
        _maxHappiness = maxHappiness;
        _happiness = _maxHappiness;
        _eat = eat;
        _canWear = canWear;
    }

    public void DecreaseHappiness()
    {
        if (_happiness > 1)
        {
            if(_wear != null) _happiness--;
            else _happiness-= 2;
        }
    }

    public void IncreaseHunger()
    {
        if (_hunger < 99)
        {
            _hunger += 2;
        }
    }
    
    public void EatFood(Food food)
    {
        _hunger -= food.hungerRestoration;
    }

    public void SetWearItem(RareItem item)
    {
        _wear = item;
    }

    public bool WearItem()
    {
        return _wear != null;
    }
    
}
