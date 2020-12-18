using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyDetection : MonoBehaviour
{
    private bool _touchAnimal = false;
    public bool touchAnimal => _touchAnimal;
    void OnTriggerEnter()
    {
        _touchAnimal = true;
    }

    void OnTriggerExit()
    {
        _touchAnimal = false;
    }
}
