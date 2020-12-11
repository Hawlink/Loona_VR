﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Savegame
{
    public string name;
    public Vector3 position;
}

[Serializable]
public class Savegames
{
    public List<Savegame> savegames = new List<Savegame>();
}


public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject _messageCanvas;

    public GameObject messageCanvas => _messageCanvas;
    
    //TODO: Remove (or at least move) the fixed rotation of the player to avoid the upside-down teleportation pb
    private GameObject _playerGameObject;
    
    private Player _player;

    public Player player => _player;

    private List<Food> _food = new List<Food>();
    
    private List<Item> _items = new List<Item>();

    public List<Item> items => _items;

    private bool _gameIsAlive = true;


    public Item GetItem(string name)
    {
        Item res = null;
        foreach(Item i in _items)
        {
            if (i.name == name) res = i;
        }
        return res;
    }

    public void SaveGame(string saveName)
    {
        
        Vector3 playerPosition = GameObject.Find("PlayerController").transform.position;

        Savegames saves;

        if (PlayerPrefs.HasKey("Savegames"))
        {
            saves = JsonUtility.FromJson<Savegames>(PlayerPrefs.GetString("Savegames"));
        }
        else
        {
            saves = new Savegames();
        }
        
        Savegame existingSave = new Savegame(){name = saveName};
        foreach(Savegame save in saves.savegames)
            if (save.name == saveName)
            {
                existingSave = save;
                saves.savegames.Remove(existingSave);
            }
        
        existingSave.position = playerPosition;
        saves.savegames.Add(existingSave);
        
        PlayerPrefs.SetString("Savegames", JsonUtility.ToJson(saves, true));
        PlayerPrefs.Save();
    }
    
    public void LoadGame(string saveName)
    {
        if (PlayerPrefs.HasKey("Savegames"))
        {
            string allSavegamesString = PlayerPrefs.GetString("Savegames");
            Savegames allSavegames = JsonUtility.FromJson<Savegames>(allSavegamesString);

            Savegame save = null;
            foreach(Savegame s in allSavegames.savegames)
                if (s.name == saveName)
                    save = s;
            
            if (save != null)
            {
                Vector3 position = save.position;
                GameObject.Find("PlayerController").transform.position = position;
            }
        }
    }

    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            DebugUtils.message = condition;
            DebugUtils.message2 = stackTrace;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        Application.logMessageReceived += Application_logMessageReceived;
        _messageCanvas.SetActive(false);
        
        _items.Add(new Food("Carrot","Prefabs/Objects/Carrot",60));
        _items.Add(new RareItem("Ribbon", "Prefabs/Objects/Ribbon", 40));

        if (MainGameCommonValues.gameName != "")
        {
            LoadGame(MainGameCommonValues.gameName);
        }
        _playerGameObject = GameObject.Find("PlayerController");
        GameObject.Find("Terrain").layer = GameObject.Find("LayerModel").layer;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color(0.204f, 0.224f, 0.263f);
        
        //Create a player and get the animals in the scene
        _player = new Player("Player 1");

        AnimalBody[] animals = FindObjectsOfType<AnimalBody>();
        //_player.addAnimal(new Animal("noname",100,_food[1]));

        StartCoroutine((GameLoop()));

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Quaternion fixedRotation = Quaternion.Euler(0, _playerGameObject.transform.rotation.eulerAngles.y,0);
        _playerGameObject.transform.rotation = fixedRotation;

        if (Input.GetKey(KeyCode.A))
        {
            GameObject inv = GameObject.Find("Inventory");
            foreach (OVRGrabbable ob in inv.GetComponentsInChildren<OVRGrabbable>())
            {
                OnGrabBegin(ob);
            }
        }
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

    public void OnGrabBegin(object grabbableObject)
    {
        DebugUtils.message2 = "GRAB BEGIN";
        OVRGrabbable grabbable = grabbableObject as OVRGrabbable;
        //If player get an inventory object
        if (grabbable != null && !grabbable.GetComponent<Rigidbody>().useGravity)
        {
            //Become a classic object 
            Destroy(grabbable.transform.Find("3DText").gameObject);
            grabbable.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            grabbable.gameObject.GetComponent<Rigidbody>().useGravity = true;
            grabbable.transform.parent = null;
            
            //Item is in hand and now no longer in the inventory
            player.removeFromInventory(grabbable.gameObject.GetComponent<ItemBody>().item);
            
            //Refresh inventory
            this.GetComponentInChildren<Watch>().DestroyCircleMenu();
            this.GetComponentInChildren<Watch>().InitializeCircleMenu();
        }
    }
    
    public void OnGrabEnd()
    {
        gameObject.GetComponentInChildren<Bag>().OnGrabEnd();
    }

    void OnTriggerExit(Collider collider)
    {
        /*
        OVRGrabbable grabbable = collider.gameObject.GetComponent<OVRGrabbable>();

        if (grabbable == grabbedObject)
        {
            grabbedObject = null;
            _messageCanvas.SetActive(false);
            message2 = "Out Canvas !";
        }*/
        
        /*
        //A grabbable object is in the bag location
        if (grabbable != null)
        {
            bool isGrabbed = GrabbableIsGrabbed(grabbable);

            if (isGrabbed)
            {
                message2 = "Out !";
                _messageCanvas.SetActive(false);
                message2 = "Out Canvas !";

            } 
        }*/
        
        /*
        if (collider.tag == "Hand")
        {
            OVRGrabber handGrabber = collider.GetComponent<OVRGrabber>();
            //if (handGrabber.grabbedObject != null)
            {
                message2 = "Out";
                _messageCanvas.SetActive(false);
            }
        }*/
    }

    private OVRGrabbable grabbedObject;
    
    /*void OnTriggerEnter(Collider collider)
    {

        OVRGrabbable grabbable = collider.gameObject.GetComponent<OVRGrabbable>();
        //A grabbable object is in the bag location
        if (grabbable != null)
        {
            bool isGrabbed = GrabbableIsGrabbed(grabbable);

            if (isGrabbed)
            {
                grabbedObject = grabbable;
                _messageCanvas.SetActive(true);
                message2 = "OK Canvas !";
            }
        }
        
        
        
        //if (collider.tag == "Hand")
        //{
        //   OVRGrabber handGrabber = collider.GetComponent<OVRGrabber>();
        // if (handGrabber.grabbedObject != null)
        //    {
        //      message2 = "OK !";
        //      _messageCanvas.SetActive(true);
        //  }
        //  else
        //  {
        //      message = "Collider! No grabbed object " + Time.time;
        //  }
        //}

    }*/
    
}
