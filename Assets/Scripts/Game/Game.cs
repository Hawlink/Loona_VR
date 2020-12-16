using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/**
 *
items	deux listes name, valeurs pour stack et inventaire	ok (name si faut)

animaux, lire body et les crées, puis feed avec valeurs (positoin + wear + hap eating ?)
pourchaque objet : type - position ?
 * 
 */

[Serializable]
public class AnimalData
{
    public string name;
    public Vector3 position;
    public Vector3 rotation;
    public int happiness;
    public int hunger;
    public string eat;
    public string canWear;
    public string wear;
}

[Serializable]
public class Savegame
{
    public string name;
    public Vector3 position;
    public List<AnimalData> animals;
    public List<string> itemsNamesInventory;
    public List<string> itemsNamesStock;
}

[Serializable]
public class Savegames
{
    public List<Savegame> savegames = new List<Savegame>();
}


/// <summary>
/// Main game behaviour
/// </summary>
public class Game : MonoBehaviour
{
    /// <summary>
    /// Canvas to put message to the user
    /// </summary>
    [SerializeField]
    private GameObject _messageCanvas;

    public GameObject messageCanvas => _messageCanvas;
    
    //TODO: Remove (or at least move) the fixed rotation of the player to avoid the upside-down teleportation pb
    private GameObject _playerGameObject;
    
    /// <summary>
    /// Player informations (stock, inventory)
    /// </summary>
    private Player _player;

    public Player player => _player;
    
    /// <summary>
    /// List of items existing in the game
    /// </summary>
    private List<Item> _items = new List<Item>();

    public List<Item> items => _items;

    private bool _gameIsAlive = true;
    
    /// <summary>
    /// Object currently grabbed by player
    /// </summary>
    private OVRGrabbable grabbedObject;

    private List<AnimalBody> _animals = new List<AnimalBody>();
    

    /// <summary>
    /// Get item by its name
    /// TODO: Replace string by item enum
    /// </summary>
    /// <param name="name">Item name</param>
    /// <returns></returns>
    public Item GetItem(string name)
    {
        Item res = null;
        foreach(Item i in _items)
        {
            if (i.name == name) res = i;
        }
        return res;
    }

    /// <summary>
    /// Get animal by his name
    /// </summary>
    /// <param name="name">Animal name</param>
    /// <returns></returns>
    public AnimalBody GetAnimal(string name)
    {
        AnimalBody res = null;
        foreach (AnimalBody ab in _animals)
        {
            if (ab.animal.name == name) res = ab;
        }
        return res;
    }
    
    /// <summary>
    /// Save game in the disk
    /// Currently only player position
    /// </summary>
    /// <param name="saveName">Save name</param>
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
        foreach (Savegame save in saves.savegames)
        {
            if (save.name == saveName)
            {
                existingSave = save;
            }
        }
        saves.savegames.Remove(existingSave);

        existingSave.position = transform.position;

        existingSave.animals = new List<AnimalData>();

        existingSave.itemsNamesInventory = new List<string>();
        existingSave.itemsNamesStock = new List<string>();
        foreach (Item item in _player.inventory)
        {
            existingSave.itemsNamesInventory.Add(item.name);
        }
        foreach (Item item in _player.stock)
        {
            existingSave.itemsNamesStock.Add(item.name);
        }

        foreach (AnimalBody ab in _animals)
        {
            AnimalData ad = new AnimalData();
            ad.eat = ab.animal.eat.name;
            ad.happiness = ab.animal.happiness;
            ad.hunger = ab.animal.hunger;
            ad.position = ab.transform.position;
            ad.rotation = ab.transform.rotation.eulerAngles;
            ad.wear = ab.animal.WearItem() ? ab.animal.wear.name : "";
            ad.canWear = ab.animal.canWear.name;
            ad.name = ab.name;
            existingSave.animals.Add(ad);
        }
        
        saves.savegames.Add(existingSave);
        
        PlayerPrefs.SetString("Savegames", JsonUtility.ToJson(saves, true));
        PlayerPrefs.Save();
    }
    
    /// <summary>
    /// Load player game
    /// Currently only player position
    /// </summary>
    /// <param name="saveName"></param>
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
                foreach (string s in save.itemsNamesInventory)
                {
                    _player.inventory.Add(GetItem(s));
                }
                foreach (string s in save.itemsNamesStock)
                {
                    _player.stock.Add(GetItem(s));
                }
                
                Vector3 position = save.position;
                GameObject.Find("PlayerController").transform.position = position;

                foreach (AnimalData ad in save.animals)
                {
                    AnimalBody concernedAnimal = GetAnimal(ad.name);
                    Food eat = GetItem(ad.eat) as Food;
                    int happiness = ad.happiness;
                    int hunger = ad.hunger;
                    Vector3 animalPosition = ad.position;
                    Vector3 animalRotation = ad.rotation;
                    string wearStr = ad.wear;
                    RareItem wear = null;
                    if (wearStr != null)
                    {
                        wear = GetItem(wearStr) as RareItem;
                    }
                    RareItem canWear = GetItem(ad.canWear) as RareItem;
                    concernedAnimal.animal.SetProperties(happiness,hunger,eat,wear,canWear);
                    concernedAnimal.transform.position = animalPosition;
                    concernedAnimal.transform.rotation = Quaternion.Euler(animalRotation);
                }
            }
        }
    }

    /// <summary>
    /// Redirect Unity logs on the VR screen
    /// </summary>
    /// <param name="condition"></param>
    /// <param name="stackTrace"></param>
    /// <param name="type"></param>
    private void Application_logMessageReceived(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Error || type == LogType.Exception)
        {
            DebugUtils.message = condition;
            DebugUtils.message2 = stackTrace;
        }
    }
    
    // Start is called before the first frame update
    void Awake()
    {
        Application.logMessageReceived += Application_logMessageReceived;
        _messageCanvas.SetActive(false);
        
        _items.Add(new Food("Carrot","Prefabs/Objects/Carrot",60));
        _items.Add(new RareItem("Ribbon", "Prefabs/Objects/Ribbon", 40, new Vector3(-0.17f,0.83f,-0.26f), new Vector3(-90,0,0) ));

        _playerGameObject = GameObject.Find("PlayerController");
        GameObject.Find("Terrain").layer = GameObject.Find("LayerModel").layer;
        Camera.main.clearFlags = CameraClearFlags.SolidColor;
        Camera.main.backgroundColor = new Color(0.204f, 0.224f, 0.263f);
        
        //Create a player 
        _player = new Player("Player 1");

        AnimalBody[] animals = FindObjectsOfType<AnimalBody>();
        foreach (AnimalBody ab in animals)
        {
            _animals.Add(ab);
        }
        
        


        StartCoroutine((GameLoop()));

    }

    IEnumerator LoadCoroutine()
    {
        yield return new WaitForSeconds(1f);
        //MainGameCommonValues.gameName = "test";
        if (MainGameCommonValues.gameName != "")
        {
            LoadGame(MainGameCommonValues.gameName);
        }
    }
    
    void Start()
    {
        StartCoroutine(LoadCoroutine());
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
        if (Input.GetKey(KeyCode.S))
        {
            SaveGame("test");
        }
    }

    /// <summary>
    /// Main loop game
    /// Every ten seconds, animals stats are updated
    /// </summary>
    /// <returns></returns>
    IEnumerator GameLoop()
    {
        while (_gameIsAlive)
        {
            yield return new WaitForSeconds(0.3f);

            foreach (AnimalBody animal in _animals)
            {
                animal.animal.IncreaseHunger();
                animal.animal.DecreaseHappiness();
            }
        }
    }

    /// <summary>
    /// Player grab an object
    /// </summary>
    /// <param name="grabbableObject">Object grabbed</param>
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
    
    /// <summary>
    /// Player drop an object
    /// </summary>
    public void OnGrabEnd()
    {
        gameObject.GetComponentInChildren<Bag>().OnGrabEnd();
    }

    /// <summary>
    /// Unused
    /// </summary>
    /// <param name="collider"></param>
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
