using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    private string _name;

    private string _prefab;

    public string name => _name;
    public string prefab => _prefab;

    public Item(string name, string prefab)
    {
        _name = name;
        _prefab = prefab;
    }

    /// <summary>
    /// Get the prefab GameObject corresponding to an item
    /// Warning : instantiate the prefab before use it to not modify directly the prefab object
    /// </summary>
    /// <returns></returns>
    public GameObject GetPrefab()
    {
        return Resources.Load(_prefab,typeof(GameObject)) as GameObject;
    }
}
