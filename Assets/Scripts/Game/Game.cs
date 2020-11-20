using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    private Player _player;

    private List<Food> _food = new List<Food>();

    private bool _gameIsAlive = true;
    
    // Start is called before the first frame update
    void Start()
    {

        _food.Add(new Food("Nuts", 30));
        _food.Add(new Food("Carrot", 50));
        
        //Create a player and get the animals in the scene
        _player = new Player("Player 1");

        AnimalBody[] animals = FindObjectsOfType<AnimalBody>();
        _player.addAnimal(new Animal("noname",100,_food[1]));

        StartCoroutine((GameLoop()));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    IEnumerator GameLoop()
    {
        while (_gameIsAlive)
        {
            yield return new WaitForSeconds(10f);

            foreach (Animal animal in _player.animals)
            {
                animal.IncreaseHunger();
                animal.DecreaseHappiness();
            }
        }
    }
    
}
