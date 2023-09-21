using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRoad : Road
{
    // Start is called before the first frame update
    void Start()
    {
        Turntable.Instance.OnRoadStatusChanged += HandleRoadStatusChanged;
        canPassCenter = false;
        endRoadTransform = transform.GetChild(transform.childCount-1);
    }

    void OnDestroy()
    {
        Turntable.Instance.OnRoadStatusChanged -= HandleRoadStatusChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleRoadStatusChanged(string roadName,bool isCanPass)
    {
        if(roadName == transform.name)
            canPassCenter = isCanPass;
    }
}
