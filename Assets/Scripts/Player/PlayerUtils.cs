using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUtils
{
    /// <summary>
    /// Create a circle menu with items
    /// </summary>
    /// <param name="inventoryList">List of items to display</param>
    /// <param name="parent">Which GameObject's transform to attach this menu</param>
    /// <param name="offset">Offset between parent and menu</param>
    /// <param name="radius">Radius of the menu circle</param>
    /// <param name="scale">Item scale multiplier</param>
    /// <param name="noGrabbable">Item on menu can't be grabbed by the player</param>
    /// <returns></returns>
    public static GameObject InitializeCircleMenu(List<Item> inventoryList, Transform parent, Vector3 offset, float radius, float scale, bool noGrabbable = false)
    {

        Dictionary<Item,int> inventory = new Dictionary<Item, int>();
        foreach (Item item in inventoryList)
        {
            if (inventory.ContainsKey(item)) inventory[item]++;
            else inventory[item] = 1;
        }

        //inventory[FindObjectOfType<Game>().items[0]] = 1;
        //inventory[FindObjectOfType<Game>().items[1]] = 3;
        

        GameObject circleInventory = new GameObject("Inventory");
        circleInventory.transform.parent = parent;
        circleInventory.transform.localPosition = offset;

        int totalItems = inventory.Count;
        int i = 0;
        foreach (KeyValuePair<Item, int> kvp in inventory)
        {
            GameObject prefab = kvp.Key.GetPrefab();
            GameObject item = GameObject.Instantiate(prefab);
            if (noGrabbable)
            {
                GameObject.Destroy(item.GetComponent<OVRGrabbable>());
                /*
                //Destroy BoxCollider to avoid pushing player
                BoxCollider toDestroy = null;
                foreach (BoxCollider bc in item.GetComponents<BoxCollider>())
                {
                    if (!bc.isTrigger) toDestroy = bc;
                }

                if(toDestroy != null) GameObject.Destroy(toDestroy);*/
            }
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            //capsule.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
            item.transform.parent = circleInventory.transform;
            float angle = (360 / (totalItems + 0.0f)) * i * Mathf.PI / 180;
            item.transform.localPosition = new Vector3(radius*Mathf.Cos(angle),0,radius*Mathf.Sin(angle));
            item.transform.localScale *= scale;
            GameObject text = new GameObject("3DText");
            TextMesh tm = text.AddComponent<TextMesh>();
            tm.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f) * scale;
            tm.transform.parent = item.transform;
            tm.transform.localPosition = new Vector3(0, 0, 0);
            tm.text = kvp.Value.ToString();
            text.AddComponent<FacingCamera>();
            i++;
        }

        return circleInventory;
    }
    
    /// <summary>
    /// Show message to the player in the screen
    /// </summary>
    /// <param name="message">The message to display</param>
    /// <param name="seconds">Time in seconds to let the message on the screen</param>
    /// <param name="game">Current Game</param>
    /// <returns></returns>
    public static IEnumerator MessageCoroutine(string message, int seconds, Game game)
    {
        game.messageCanvas.SetActive(true);
        game.messageCanvas.GetComponentInChildren<Text>().text = message;
        yield return new WaitForSeconds(seconds);
        game.messageCanvas.SetActive(false);
    }

}
