using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.position,-1);
        }else
            if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.position,1);
        }
    }
}
