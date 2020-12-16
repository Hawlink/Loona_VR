using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Link between an item and his physical representation
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class ItemBody : MonoBehaviour
{

    /// <summary>
    /// Item corresponding to this body
    /// </summary>
    [SerializeField]
    private Item _item;

    /// <summary>
    /// Object type
    /// (Little redundancy with _item but allow to edit the object type in Unity editor)
    /// </summary>
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

    /// <summary>
    /// Create the body in function of the item type
    /// </summary>
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
        if (transform.parent?.GetComponent<AnimalBody>() != null)
        {
            transform.localPosition = (item as RareItem).wornPosition;
            transform.localRotation = Quaternion.Euler((item as RareItem).wornRotation);
        }
    }
}


/// <summary>
/// List of item types in the game
/// </summary>
public enum ObjectType
{
    Ribbon,
    Carrot
}