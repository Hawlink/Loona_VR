using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generate food item randomly
/// </summary>
public class FoodGenerator : MonoBehaviour
{

    [SerializeField] private ObjectType _type;
    [SerializeField] private float _frequency = 50;
    [SerializeField] private float _radius = 10;
    [SerializeField] private bool _oneTime = true;
    
    
    // Start is called before the first frame update
    void Start()
    {

        if (_oneTime)
        {
            StartCoroutine(GeneratorNoLoop());
        }
        else
        {
            StartCoroutine(GeneratorLoop());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Generate()
    {
        Vector3 offset = Random.insideUnitSphere * _radius;
        offset.y = transform.position.y;

        //TODO: IT'S VERY HORRIBLE MUST BE CHANGED, ONLY FOR TEST
        Item item = GameObject.FindObjectOfType<Game>().GetItem(_type.ToString());

        GameObject prefab = Resources.Load(item.prefab, typeof(GameObject)) as GameObject;
        GameObject spawnedItem = Instantiate(prefab,transform.position + offset ,Quaternion.identity);
        DebugUtils.message = "GENERATED FOOD";
        DebugUtils.message2 = "GENERATED FOOD";
        DebugUtils.message3 = "GENERATED FOOD";
    }
    
    IEnumerator GeneratorLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(_frequency);
            Generate();

        }
    }
    
    IEnumerator GeneratorNoLoop()
    {
        yield return new WaitForSeconds(_frequency);
        Generate();
    }

}
