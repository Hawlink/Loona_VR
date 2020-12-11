using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(OVRGrabbable))]
public class ItemBody : MonoBehaviour
{

    [SerializeField]
    private Item _item;

    [SerializeField]
    private ObjectType _type;
    
    public ObjectType type
    {
        get => _type;
        set => _type = value;
    }

    public Item item
    {
        get => _item;
        set => _item = value;
    }

    public void InitializeItem()
    {
        switch (_type)
        {
            case ObjectType.Ribbon:
                _item = GameObject.FindObjectOfType<Game>().GetItem("Ribbon");
                break;
            case ObjectType.Carrot:
                _item = GameObject.FindObjectOfType<Game>().GetItem("Carrot");
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        InitializeItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public enum ObjectType
{
    Ribbon,
    Carrot
}