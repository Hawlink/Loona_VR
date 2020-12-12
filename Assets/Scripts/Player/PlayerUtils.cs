using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUtils
{
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
}
