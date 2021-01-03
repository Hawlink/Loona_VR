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
        _canvasPopup.SetActive(false);
        //BtnLoadGameOnClick();


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
            
            /*GameObject button2 = Resources.Load("UI/WoodButton", typeof(GameObject)) as GameObject;
            button2 = Instantiate(button2, GameObject.Find("LoadPanel").transform);
            button2.transform.GetChild(0).GetComponent<Text>().text = "baepsae";
            button2.GetComponent<RectTransform>().position = new Vector3(button2.GetComponent<RectTransform>().position.x, button2.GetComponent<RectTransform>().position.y, 0);
            button2.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            button2.GetComponent<Button>().onClick.AddListener(delegate { BtnSavedGameOnClick("baepsae"); });*/
            
            foreach (Savegame s in allSavegames.savegames)
            {
                GameObject button = Resources.Load("UI/WoodButton", typeof(GameObject)) as GameObject;
                button = Instantiate(button,GameObject.Find("LoadPanel").transform);
                button.transform.GetChild(0).GetComponent<Text>().text = s.name;
                button.GetComponent<RectTransform>().position = new Vector3(button.GetComponent<RectTransform>().position.x, button.GetComponent<RectTransform>().position.y, 0);
                button.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
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
