using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start");
        DontDestroyOnLoad(this.gameObject);
        this.gameObject.tag = "DontDestroyOnLoad";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
