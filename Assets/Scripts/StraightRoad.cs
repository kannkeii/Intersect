using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StraightRoad : Road
{
    // Start is called before the first frame update
    void Start()
    {
        Turntable.Instance.OnRoadStatusChanged += HandleRoadStatusChanged;
        canPassCenter = false;
        endRoadTransform = transform.GetChild(transform.childCount-2);
    }

    void OnDestroy()
    {
        Turntable.Instance.OnRoadStatusChanged -= HandleRoadStatusChanged;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleRoadStatusChanged(string passRoadName,string exitRoadName, bool isCanPass)
    {
        //Debug.Log("passRoadName:" + passRoadName + ",exitRoadName:" + exitRoadName+",Time:"+Time.deltaTime);

        if(passRoadName == transform.name)
            canPassCenter = isCanPass;
        if (exitRoadName == transform.name)
            ChangeRoadText(exitRoadName);

    }

    private void ChangeRoadText(string roadName)
    {
        Transform textObj = transform.Find("Canvas").GetChild(0);

        TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();

        

        if (dir == Road.DIR.DIR_IN)
        {
            Debug.Log("RoadError");
        }
        else
        {
            textObj.gameObject.SetActive(true);
            text.color = Color.green;
            text.text = "Å´";
        }
    }
}
