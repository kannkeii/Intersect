using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove :Train
{
    private Vector3 tagetPosition;
    // Start is called before the first frame update
    void Start()
    {
        tagetPosition = new Vector3(tagetPosition.x, transform.position.y, tagetPosition.z);
        dir = DIR.DIR_IN;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position != tagetPosition)
        {
            switch (dir)
            {
                case DIR.DIR_IN:
                    InMove();
                    break;
                case DIR.DIR_OUT:
                    OutMove();
                    break;
                default:
                    Debug.Log("error:" + dir);
                    break;
            }

            
        }  
        
    }

    void InMove()
    {
        transform.position = Vector3.MoveTowards(transform.position, tagetPosition, speed * Time.deltaTime);
    }

    void OutMove()
    {

    }




    void OnTriggerEnter(Collider other)
    {
        if (other == null)
            return;

        Road road = null;
        if (!other.gameObject.TryGetComponent(out road))
        {
            return;
        }

        dir = (DIR)road.dir;//共通なDIRを宣言、定義する必要がある
    }
}
