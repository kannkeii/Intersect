using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainMove :Train
{
    public event Action OnWaitAtRoadEnd;

    private Vector3 tagetPosition;

    private bool atPlayer;
    private bool haveTrainMoveToCenter;
    private bool canBeLeaveFromCenter;

    // Start is called before the first frame update
    void Start()
    {
        actionMode = ACTION_MODE.ACTION_MODE_RUNING;
        Turntable.Instance.OnInsideTrainAngleChanged += HandleInsideTrainAngleChanged;
        tagetPosition = new Vector3(tagetPosition.x, transform.position.y, tagetPosition.z);
        dir = DIR.DIR_IN;
        defaultSpeed = speed;
        atPlayer = false;
        haveTrainMoveToCenter = false;
        canBeLeaveFromCenter = false;
    }

    private void HandleInsideTrainAngleChanged(Transform trainTransform)
    {
        canBeLeaveFromCenter = true;
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
        transform.position = Vector3.MoveTowards(transform.position, tagetPosition, speed * Time.deltaTime);
    }

    void HaveTrainMoveToCenter(string name)
    {
        //Debug.Log("HaveTrainMoveToCenter:"+ name);
        if(name != transform.name)
        {
            haveTrainMoveToCenter = true;
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other == null)
    //        return;

    //    if (other.tag == "Player")
    //    {
    //        comeRoad.GetComponent<StraightRoad>().canPassCenter = true;
    //    }
    //}


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

            if (actionMode == ACTION_MODE.ACTION_MODE_RUNING3)
            {
                transform.parent = exitRoad.transform.parent;

                tagetPosition = exitRoad.GetComponent<StraightRoad>().endRoadTransform.position;

                speed = defaultSpeed;

                transform.LookAt(tagetPosition);

                Vector2 trainVec2 = new Vector2(transform.position.x, transform.position.z);
                Vector2 endVec2 = new Vector2(tagetPosition.x, tagetPosition.z);

                float dis = Vector2.Distance(trainVec2, endVec2);

                if (dis < .6f)
                {
                    speed = 0;

                    GameManager.Instance.thisLevelTrainNum++;

                    if (GameManager.Instance.thisLevelTrainNum >= GameManager.Instance.thisLevelTrainAllNum)
                    {
                        GameManager.Instance.thisLevelTrainNum = 0;

                        GameManager.Instance.thisLevelTrainAllNum = 0;

                        GameManager.Instance.nowLevelIndex++;

                        GameManager.Instance.GameOver();
                    }

                    Destroy(gameObject);
                }

                return;
            }

            if (actionMode == ACTION_MODE.ACTION_MODE_WAIT_AT_CENTER)
            {
                dir = DIR.DIR_OUT;

                speed = 0;

                if (canBeLeaveFromCenter == false)
                {
                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        obj.SendMessage("CanTrainLeaveToCenter", obj, SendMessageOptions.DontRequireReceiver);
                    }

                    return;
                }

                actionMode = ACTION_MODE.ACTION_MODE_RUNING3;

                return;
            }
        }



        if (other.tag == "Player")
        {
            atPlayer = true;
            if (actionMode == ACTION_MODE.ACTION_MODE_RUNING)
            {
                OnWaitAtRoadEnd?.Invoke();
                actionMode = ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD_END;
                speed = 0;
                return;
            }

            if(actionMode == ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD_END)
            {
                

                return;
            }

            if (actionMode == ACTION_MODE.ACTION_MODE_RUNING2)
            {
                if (haveTrainMoveToCenter == true)
                {
                    //other.GetComponent<StraightRoad>().canPassCenter = false;
                    actionMode = ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD_END;
                    return;
                }

                if (haveTrainMoveToCenter == false)
                {
                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
                    {
                        obj.SendMessage("HaveTrainMoveToCenter",obj, SendMessageOptions.DontRequireReceiver);
                    }

                    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Train"))
                    {
                        obj.SendMessage("HaveTrainMoveToCenter", obj.name, SendMessageOptions.DontRequireReceiver);
                    }
                }

                speed = defaultSpeed;

                Vector2 trainVec2 = new Vector2(transform.position.x, transform.position.z);
                Vector2 playerVec2 = new Vector2(other.transform.position.x, other.transform.position.z);

                float dis = Vector2.Distance(trainVec2, playerVec2);

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
