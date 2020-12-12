using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Watch : MonoBehaviour
{

    private GameObject m_canvas;

    private GameObject _circleInventory;
    
    private int offset = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GameObject.Find("WatchCanvas");
        m_canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_canvas.activeSelf)
        {
            float lastXPosition = -1;
            if (OVRInput.Get(OVRInput.RawButton.A))
            {
                offset -= 10;
            }

            if (OVRInput.Get(OVRInput.RawButton.B))
            {
                offset += 10;
                

                /*
                float currentXPosition = GameObject.Find("RightHandAnchor").transform.position.x;
                if (lastXPosition != -1)
                {
                    currentOffset += currentXPosition - lastXPosition;
                    foreach (GameObject item in _itemsInventory)
                    {
                        Vector3 rotation = item.transform.rotation.eulerAngles;
                        rotation += new Vector3(0, currentXPosition - lastXPosition, 0);
                        item.transform.rotation = Quaternion.Euler(rotation);
                    }
                }
                lastXPosition = currentXPosition;*/
            }
            
            ApplyRotationOnCircleMenu();
            
        }
        
    }

    public void ApplyRotationOnCircleMenu()
    {
        Vector3 rotation = _circleInventory.transform.rotation.eulerAngles;
        rotation = new Vector3(rotation.x, offset, rotation.z);
        _circleInventory.transform.rotation = Quaternion.Euler(rotation);
    
    }

    public void InitializeCircleMenu()
    {

        _circleInventory = PlayerUtils.InitializeCircleMenu(GameObject.FindObjectOfType<Game>().player.inventory,
            m_canvas.transform.parent.parent.parent.parent, new Vector3(-0.6f, 0.65f, 1f), 0.6f, 0.75f);
        
        /*
        Dictionary<Item,int> inventory = new Dictionary<Item, int>();
        foreach (Item item in GameObject.FindObjectOfType<Game>().player.inventory)
        {
            if (inventory.ContainsKey(item)) inventory[item]++;
            else inventory[item] = 1;
        }

        //inventory[FindObjectOfType<Game>().items[0]] = 1;
        //inventory[FindObjectOfType<Game>().items[1]] = 3;
        

        _circleInventory = new GameObject("Inventory");
        _circleInventory.transform.parent = m_canvas.transform.parent.parent.parent.parent;
        _circleInventory.transform.localPosition = new Vector3(-0.6f, 0.65f, 1f);

        int totalItems = inventory.Count;
        int i = 0;
        foreach (KeyValuePair<Item, int> kvp in inventory)
        {
            GameObject prefab = kvp.Key.GetPrefab();
            GameObject item = Instantiate(prefab);
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            //capsule.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            item.transform.parent = _circleInventory.transform;
            float angle = (360 / (totalItems + 0.0f)) * i * Mathf.PI / 180;
            item.transform.localPosition = new Vector3(0.7f*Mathf.Cos(angle),0,0.7f*Mathf.Sin(angle));
            item.transform.localScale *= 0.75f;
            GameObject text = new GameObject("3DText");
            TextMesh tm = text.AddComponent<TextMesh>();
            tm.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            tm.transform.parent = item.transform;
            tm.transform.localPosition = new Vector3(0, 0, 0);
            tm.text = kvp.Value.ToString();
            i++;
        }
        */
        ApplyRotationOnCircleMenu();

    }

    public void DestroyCircleMenu()
    {
        Destroy(_circleInventory);
        _circleInventory = null;
    }

    void OnTriggerEnter(Collider collider)
    {
        /*
        if (collider.gameObject.tag == "Hand" && !m_canvas.activeSelf)
        {
            m_canvas.SetActive(true);
            InitializeCircleMenu();
        }*/
    }

    public void BtnQuitWatchOnClick()
    {
        m_canvas.SetActive(false);
        DestroyCircleMenu();
    }

    public void BtnSaveOnClick()
    {
        string saveName = GameObject.Find("InputSaveName").GetComponent<InputField>().text;
        GameObject.FindObjectOfType<Game>().SaveGame(saveName);
    }

    public void BtnQuitGameOnClick()
    {
        SceneManager.LoadScene(0);
    }

    public void BtnBaseOnClick()
    {
        GameObject playerBaseLocation = GameObject.FindGameObjectWithTag("PlayerBase");
        GameObject.FindObjectOfType<Game>().transform.position = playerBaseLocation.transform.position;
    }
}