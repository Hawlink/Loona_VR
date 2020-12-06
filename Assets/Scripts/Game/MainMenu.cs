using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class MainGameCommonValues
{
    public static string gameName { get; set; } = "";
}

public class MainMenu : MonoBehaviour
{

    private GameObject _canvasPopup;
    
    // Start is called before the first frame update
    void Start()
    {
        _canvasPopup = GameObject.Find("LoadPanel");
        _canvasPopup.SetActive(true);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnLaunchOnClick()
    {
        SceneManager.LoadScene(1);
    }
    

    public void BtnSavedGameOnClick(string gameName)
    {
        MainGameCommonValues.gameName = gameName;
        SceneManager.LoadScene(1);
    }
    public void BtnLoadGameOnClick()
    {
        _canvasPopup.SetActive(true);

        foreach (Transform child in GameObject.Find("LoadPanel").transform)
        {
            if (child.GetComponent<Button>() != null)
                GameObject.Destroy(child.gameObject);
        }

        if (PlayerPrefs.HasKey("Savegames"))
        {
            string allSavegamesString = PlayerPrefs.GetString("Savegames");
            Savegames allSavegames = JsonUtility.FromJson<Savegames>(allSavegamesString);
            foreach (Savegame s in allSavegames.savegames)
            {
                GameObject button = Resources.Load("UI/WoodButton", typeof(GameObject)) as GameObject;
                button = Instantiate(button);
                button.transform.parent = GameObject.Find("LoadPanel").transform;
                button.transform.GetChild(0).GetComponent<Text>().text = s.name;
                button.GetComponent<Button>().onClick.AddListener(delegate { BtnSavedGameOnClick(s.name); });
            }
        }
        else
        {
            GameObject label = Resources.Load("UI/CaligraphiedLabel", typeof(GameObject)) as GameObject;
            label = Instantiate(label);
            label.transform.parent = GameObject.Find("LoadPanel").transform;
            label.transform.GetComponent<Text>().text = "Aucune partie sauvegardée";
        }

    }

    public void BtnQuitOnClick()
    {
        Application.Quit();
    }
}
