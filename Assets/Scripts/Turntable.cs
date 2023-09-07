using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable : MonoBehaviour
{
    public static Vector3 LocalScale {get {return localScale;} }

    static Vector3 localScale = default;
    // Start is called before the first frame update
    void Start()
    {
        localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
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
