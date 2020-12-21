using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garland : MonoBehaviour
{

    private List<Color> _colors;
    
    // Start is called before the first frame update
    void Start()
    {
        _colors = new List<Color>();
        _colors.Add(new Color(1,0,0));
        _colors.Add(new Color(0,1,0));
        _colors.Add(new Color(0,0,1));
        StartCoroutine(ColorChange());
    }

    IEnumerator ColorChange()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Color newColor = _colors[Random.Range(0, _colors.Count)];
            transform.GetComponent<MeshRenderer>().material.color = newColor;
            transform.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",newColor);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
