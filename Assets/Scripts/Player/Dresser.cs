using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Dresser : MonoBehaviour
{

    private Vector3 _initialPosition;

    private GameObject _pullableDrawer;

    private float _minX;

    private float _rotationInventory;
    private float _rotationStock;
    
    [SerializeField]
    private GameObject _dresserInventoryLocation;
    [SerializeField]
    private GameObject _dresserStockLocation;
    
    private GameObject _inventoryCircle;
    private GameObject _stockCircle;

    private List<GameObject> _uiHelpers;


    // Start is called before the first frame update
    void Start()
    {
        _pullableDrawer = gameObject.transform.Find("Drawer_002").gameObject;
        _minX = _pullableDrawer.transform.position.x;

        _initialPosition = transform.position;
        
        _rotationInventory = 0;
        _rotationStock = 0;
        
        _uiHelpers = new List<GameObject>();
        foreach (TextMesh uiInfo in _pullableDrawer.GetComponentsInChildren<TextMesh>())
        {
            _uiHelpers.Add(uiInfo.gameObject);
            uiInfo.gameObject.SetActive(false);
        }
        
    }

    GameObject GetClosestInventoryItem(GameObject itemsCircle, Transform hand)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = hand.position;
        for (int i = 0; i < itemsCircle.transform.childCount; i++)
        {
            Transform potentialTarget = itemsCircle.transform.GetChild(i);
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        GameObject res = null;
        if (bestTarget != null)
        {
            res = bestTarget.gameObject;
        }
        return res;
    }
    
    // Update is called once per frame
    void Update()
    {
        //transform.position = _initialPosition;

        if (_inventoryCircle != null && _stockCircle != null)
        {
            if (OVRInput.Get(OVRInput.RawButton.B))
            {
                _rotationStock -= 7;
                ApplyRotationCircle(_stockCircle, _rotationStock);
            }

            if (OVRInput.Get(OVRInput.RawButton.Y))
            {
                _rotationInventory +=7;
                ApplyRotationCircle(_inventoryCircle, _rotationInventory);
            }
            
            
            foreach(TextMesh text in _inventoryCircle.GetComponentsInChildren<TextMesh>()) text.color = new Color(1,1,1);
            foreach(TextMesh text in _stockCircle.GetComponentsInChildren<TextMesh>()) text.color = new Color(1,1,1);

            GameObject closestInventoryItem = GetClosestInventoryItem(_inventoryCircle, GameObject.Find("CustomHandLeft").transform);
            if(closestInventoryItem != null)
                closestInventoryItem.GetComponentInChildren<TextMesh>().color = new Color(1, 0, 0);
            GameObject closestStackItem = GetClosestInventoryItem(_stockCircle, GameObject.Find("CustomHandRight").transform);
            if(closestStackItem)
                closestStackItem.GetComponentInChildren<TextMesh>().color = new Color(1, 0, 0);
            
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                //Pass object in the stack
                GameObject selectedGameObject =
                    GetClosestInventoryItem(_inventoryCircle, GameObject.Find("CustomHandLeft").transform);
                if (selectedGameObject != null)
                {
                    Item selectedItem = selectedGameObject.GetComponent<ItemBody>().item;
                    GameObject.FindObjectOfType<Game>().player.removeFromInventory(selectedItem);
                    GameObject.FindObjectOfType<Game>().player.addToStock(selectedItem);
                    DestroyCirclesMenu();
                    CreateCirclesMenu();
                }
            }

            if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                //Pass object in the inventory if possible
                GameObject selectedGameObject =
                    GetClosestInventoryItem(_stockCircle, GameObject.Find("CustomHandRight").transform);
                if (selectedGameObject != null)
                {
                    Item selectedItem = selectedGameObject.GetComponent<ItemBody>().item;
                    GameObject.FindObjectOfType<Game>().player.removeFromStock(selectedItem);
                    GameObject.FindObjectOfType<Game>().player.addToInventory(selectedItem);
                    DestroyCirclesMenu();
                    CreateCirclesMenu();
                }

            }
        }

    }
    
    public void ApplyRotationCircle(GameObject circle, float rotationOffset)
    {
        Vector3 rotation = circle.transform.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, rotationOffset, rotation.z);
        circle.transform.rotation = Quaternion.Euler(rotation);
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Hand")
        {
            if (OVRInput.Get(OVRInput.RawButton.A))
            {
                Vector3 newPosition = _pullableDrawer.transform.position;
                newPosition.x = collider.gameObject.transform.position.x;
                if(newPosition.x > _minX)
                    _pullableDrawer.transform.position = newPosition;
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Hand" && _inventoryCircle != null && _stockCircle != null)
        {
            DestroyCirclesMenu();
        }
    }

    private void CreateCirclesMenu()
    {
        _inventoryCircle = PlayerUtils.InitializeCircleMenu(GameObject.FindObjectOfType<Game>().player.inventory,
            _dresserInventoryLocation.transform, new Vector3(0,0,0), 0.2f, 0.45f, true);
        _stockCircle = PlayerUtils.InitializeCircleMenu(GameObject.FindObjectOfType<Game>().player.stock,
            _dresserStockLocation.transform,new Vector3(0,0,0), 0.2f, 0.45f, true);
            
        ApplyRotationCircle(_inventoryCircle,_rotationInventory);
        ApplyRotationCircle(_stockCircle,_rotationStock);
        
        foreach (GameObject go in _uiHelpers)
        {
            go.SetActive(true);
        }

        StartCoroutine(PlayerUtils.MessageCoroutine("Cliquer les items sélectionnés pour les faire passer dans l'inventaire et inversement", 6,GameObject.FindObjectOfType<Game>()));

    }

    private void DestroyCirclesMenu()
    {
        Destroy(_inventoryCircle);
        Destroy(_stockCircle);
        _inventoryCircle = null;
        _stockCircle = null;
        foreach (GameObject go in _uiHelpers)
        {
            go.SetActive(false);
        }
    }
    
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Hand" && _inventoryCircle == null && _stockCircle == null)
        {
            CreateCirclesMenu();
        }
    }
}
