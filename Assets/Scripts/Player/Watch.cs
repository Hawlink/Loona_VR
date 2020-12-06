using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Watch : MonoBehaviour
{

    private GameObject m_canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        m_canvas = GameObject.Find("WatchCanvas");
        m_canvas.SetActive(false);
        
        
        
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
        string saveName = GameObject.Find("InputSaveName").GetComponent<InputField>().text;
        GameObject.FindObjectOfType<Game>().SaveGame(saveName);
    }
}
