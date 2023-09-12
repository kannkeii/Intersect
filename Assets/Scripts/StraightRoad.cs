using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRoad : Road
{


    // Start is called before the first frame update
    void Start()
    {
        endRoadTransform = transform.GetChild(transform.childCount-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
