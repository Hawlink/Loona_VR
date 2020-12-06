using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{

    private GameObject m_canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GameObject.Find("WatchCanvas");
        m_canvas.SetActive(false);
        
        
        string loadedPositionData = PlayerPrefs.GetString("PlayerPosition");
        Vector3 playerPosition = JsonUtility.FromJson<Vector3>(loadedPositionData);
        GameObject.Find("PlayerController").transform.position = playerPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Hand")
        {
            m_canvas.SetActive(true);
        }
    }

    public void BtnQuitWatchOnClick()
    {
        m_canvas.SetActive(false);
    }

    public void BtnSaveOnClick()
    {
        Vector3 playerPosition = GameObject.Find("PlayerController").transform.position;
        string savePositionData = JsonUtility.ToJson(playerPosition);
        PlayerPrefs.SetString("PlayerPosition", savePositionData);
    }
}
