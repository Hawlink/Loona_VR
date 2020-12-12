using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Force GameObject to always face the camera (for text for instance)
/// </summary>
public class FacingCamera : MonoBehaviour
{
    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
    }
}
