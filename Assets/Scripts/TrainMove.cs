using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove :Train
{
    public event System.Action OnWaitAtRoadEnd;
    //public event System.Action OnNotWaitAtRoadEnd;

    private Vector3 tagetPosition;

    private bool atPlayer;
    private bool haveTrainMoveToCenter;

    // Start is called before the first frame update
    void Start()
    {
        tagetPosition = new Vector3(tagetPosition.x, transform.position.y, tagetPosition.z);
        dir = DIR.DIR_IN;
        defaultSpeed = speed;
        atPlayer = false;
        haveTrainMoveToCenter = false;
        //actionMode = ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD;
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

    void HaveTrainMoveToCenter(string name)
    {
        Debug.Log("HaveTrainMoveToCenter:"+ name);
        if(name != transform.name)
        {
            haveTrainMoveToCenter = true;
        }
    }


    void OnTriggerStay(Collider other)
    {

        if (other == null)
            return;

        if(other.tag == "Road")
        {
            if (actionMode == ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD_END)
            {
                if(other.GetComponent<StraightRoad>().canPassCenter)
                {
                    actionMode = ACTION_MODE.ACTION_MODE_RUNING2;

                    return;
                }
            }
        }



        if (other.tag == "Player")
        {
            atPlayer = true;
            if (actionMode == ACTION_MODE.ACTION_MODE_RUNING)
            {
                actionMode = ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD_END;
                speed = 0;

                return;
            }

            if(actionMode == ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD_END)
            {
                //actionMode = ACTION_MODE.ACTION_MODE_RUNING2;
                

                OnWaitAtRoadEnd?.Invoke();

                return;
            }

            //else
            //{
            //
            //    OnNotWaitAtRoadEnd?.Invoke();
            //}

            if (actionMode == ACTION_MODE.ACTION_MODE_RUNING2)
            {
                if (haveTrainMoveToCenter == true)
                {
                    return;
                }

                if (haveTrainMoveToCenter == false)
                {
                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Train"))
                    {
                        obj.SendMessage("HaveTrainMoveToCenter", obj.name, SendMessageOptions.DontRequireReceiver);
                    }
                }

                speed = defaultSpeed;

                Vector2 trainVec2 = new Vector2(transform.position.x, transform.position.z);
                Vector2 PlayerVec2 = new Vector2(other.transform.position.x, other.transform.position.z);

                float dis = Vector2.Distance(trainVec2, PlayerVec2);

                if (dis < .6f)
                {
                    actionMode = ACTION_MODE.ACTION_MODE_WAIT_AT_CENTER;
                    transform.parent = other.transform;
                    speed = 0;
                }
                return;
            }

            
        }

        Road road = null;
        if (!other.gameObject.TryGetComponent(out road) || atPlayer==true)
        {
            return;
        }

        

        dir = (DIR)road.dir;//共通なDIRを宣言、定義する必要がある


        if (actionMode == ACTION_MODE.ACTION_MODE_RUNING)
        {
            speed = defaultSpeed;
        }else
        {
            speed = 0;
        }
    }
}
