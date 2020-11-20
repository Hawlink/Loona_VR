using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string _name;

    private List<Animal> _animals = new List<Animal>();

    private List<Item> _inventory = new List<Item>();

    private List<Item> _stock = new List<Item>();

    public List<Animal> animals => _animals;

    public Player(string name)
    {
        _name = name;
    }

    public void addAnimal(Animal animal)
    {
        _animals.Add(animal);
    }
    
    public void addToInventory(Item item)
    {
        _inventory.Add(item);
    }

    public void addToStock(Item item)
    {
        _stock.Add(item);
    }

    public void playWithAnimal()
    {
        
    }

    public void feedAnimal()
    {
        
    }
}
