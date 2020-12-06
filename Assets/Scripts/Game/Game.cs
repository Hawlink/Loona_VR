using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    //TODO: Remove (or at least move) the fixed rotation of the player to avoid the upside-down teleportation pb
    private GameObject _playerGameObject;
    
    private Player _player;

    private List<Food> _food = new List<Food>();

    private bool _gameIsAlive = true;

    
    
    // Start is called before the first frame update
    void Start()
    {

        _playerGameObject = GameObject.Find("PlayerController");
        GameObject.Find("Terrain").layer = GameObject.Find("LayerModel").layer;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color(0.204f, 0.224f, 0.263f);


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
        Quaternion fixedRotation = Quaternion.Euler(0, _playerGameObject.transform.rotation.eulerAngles.y,0);
        _playerGameObject.transform.rotation = fixedRotation;
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
