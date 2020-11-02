using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    
    private float m_speed = 60f;
    private float m_rotation_speed = 80f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += transform.forward * m_speed * Time.deltaTime;
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0,-m_rotation_speed * Time.deltaTime,0));
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0,m_rotation_speed * Time.deltaTime,0));
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += -transform.forward * m_speed * Time.deltaTime;
        }
        
    }
}
