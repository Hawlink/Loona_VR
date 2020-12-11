using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string _name;

    private List<Animal> _animals = new List<Animal>();

    private List<Item> _inventory = new List<Item>();

    private List<Item> _stock = new List<Item>();

    public List<Item> inventory => _inventory;

    public List<Animal> animals => _animals;

    public Player(string name)
    {
        _name = name;
        
        _inventory.Add(GameObject.FindObjectOfType<Game>().items[0]);
        _inventory.Add(GameObject.FindObjectOfType<Game>().items[0]);
        _inventory.Add(GameObject.FindObjectOfType<Game>().items[0]);
        _inventory.Add(GameObject.FindObjectOfType<Game>().items[1]);
        _inventory.Add(GameObject.FindObjectOfType<Game>().items[1]);

    }

    public void addAnimal(Animal animal)
    {
        _animals.Add(animal);
    }


    public void removeFromInventory(Item item)
    {
        _inventory.Remove(item);
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
