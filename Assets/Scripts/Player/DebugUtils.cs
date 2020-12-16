using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Show three debugs messages in the VR view
/// </summary>
public class DebugUtils : MonoBehaviour
{
    
    public static string message = "";
    public static string message2 = "";
    public static string message3 = "";

    void OnGUI()
    {
        GUI.Label(new Rect(170, 350, 1000, 80), message);
        GUI.Label(new Rect(170, 450, 1000, 80), message2);
        GUI.Label(new Rect(170, 550, 400, 80), message3);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
