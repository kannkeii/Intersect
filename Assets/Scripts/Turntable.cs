using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Turntable : MonoBehaviour
{
    public static Vector3 LocalScale { get { return localScale; } }

    public int roadMaxNum;//�擾���������W�̐�

    static Vector3 localScale = default;

    public GameObject RoadObject;



    public static Turntable Instance { get { return instance; } }//private set; }//�ǂݍ��݁iget�j�����̏ꍇ�Afunc{get;} = var���\

    private static Turntable instance = null;

    public delegate void RoadStatusChangedHandler(string roadName, string exitName, bool isCanPass);

    public event RoadStatusChangedHandler OnRoadStatusChanged;

    public delegate void InsideTrainAngleChangedHandler(Transform trainTransform);

    public event InsideTrainAngleChangedHandler OnInsideTrainAngleChanged;

    private bool haveTrainMoveToCenter;

    private bool canTrainLeaveToCenter;

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Turntable�̃C���X�^���X�����s���܂����B");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        haveTrainMoveToCenter = false;
        canTrainLeaveToCenter = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(transform.position, -1);
        } else
            if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(transform.position, 1);
        }

        if(canTrainLeaveToCenter == false)
            CheckAngle();
        else
            CheckAngle2();

    }

    void CheckAngle()
    {
        float tolerance = 6.0f; // 精度
        float[] angles = new float[4];
        angles[0] = 0f; // 上
        angles[1] = 180f; // 下
        angles[2] = 90f; // 左
        angles[3] = 270f; // 右

        foreach (Component com in GenerateTrack.Instance.roadsComponent)
        {
            if (setRoad(com.name, false)) continue;

            OnRoadStatusChanged?.Invoke(com.name, default, false);
        }

        foreach (Component com in GenerateTrack.Instance.roadsComponent)
        {
            float angle = com.transform.rotation.eulerAngles.y;

            foreach (float targetAngle in angles)
            {
                float adjustedTargetAngle = (targetAngle + transform.rotation.eulerAngles.y) % 360;

                if (Mathf.Abs(angle - adjustedTargetAngle) < tolerance || Mathf.Abs(angle - adjustedTargetAngle - 180) < tolerance || Mathf.Abs(angle - adjustedTargetAngle + 180) < tolerance)
                {
                    if (haveTrainMoveToCenter == false)
                    {
                        if (setRoad(com.name, true)) continue;

                        OnRoadStatusChanged?.Invoke(com.name, default, true);
                    }
                    break;
                }
            }
        }
    }

    void CheckAngle2()
    {
        float tolerance = 6.0f; // 精度

        foreach(Transform t in transform)
        {
            if(t.CompareTag("Train"))
            {
                Vector3 dir = t.GetComponent<TrainMove>().endRoadTransform.position - t.transform.position;

                dir.Normalize();

                float angle = Vector3.Angle(t.transform.forward, dir);


                if (angle < tolerance)
                {
                    OnInsideTrainAngleChanged?.Invoke(t);
                }
                return;
            }
        }
    }

    bool setRoad(string enterRoad,bool flag)
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Train"))
            {
                Train train = child.GetComponent<Train>();

                OnRoadStatusChanged?.Invoke(enterRoad, train.exitRoad.name, flag);
                return true;
            }
        }

        return false;
    }

    void HaveTrainMoveToCenter(GameObject obj)
    {
        Debug.Log("HaveTrainMoveToCenter_Turntable");
        haveTrainMoveToCenter = true;
        //
        //if (name != transform.name)
        //{
        //    haveTrainMoveToCenter = true;
        //}
    }

    void CanTrainLeaveToCenter(GameObject obj)
    {
        Debug.Log("CanTrainLeaveToCenter_Turntable");
        canTrainLeaveToCenter = true;
    }

    public void Generate()
    {
        
        float radius = transform.localScale.x;//�~�̔��a
        Vector2 center = new Vector2(transform.position.x, transform.position.z);//transform.position;//�~�̒��S
        
        if (roadMaxNum <= 0) roadMaxNum = 2;
        else if (roadMaxNum % 2 != 0) roadMaxNum++;
        float minAngle = 45f;//�_�Ɠ_�̍ŏ��Ԋu(�p�x)

        List<Vector2> points = CirclePointsGenerator.GeneratePoints(radius, center, roadMaxNum, minAngle);

        int roadCnt = 0;
        foreach (var point in points)
        {
            float angleInDegrees = CirclePointsGenerator.GetPointAngle(center, point);

            Transform transform = Instantiate(RoadObject).transform;//GameObject.CreatePrimitive(PrimitiveType.Cube).transform;//Debug

            Road.DIR dir = Road.DIR.DIR_IN;

            if(roadCnt % 2 != 0)
            {
                dir = Road.DIR.DIR_OUT;
            }

            transform.GetComponent<Road>().dir = dir;

            transform.parent = this.transform.parent;

            
            //transform.position = new Vector3(point.x, 0, point.y);

            //transform.GetChild(0).eulerAngles = new Vector3(0,  angleInDegrees, 0);
            transform.rotation = Quaternion.Euler(0, transform.rotation.y + angleInDegrees, 0);

            transform.name = RoadObject.name + "_" + roadCnt;

            Transform textObj = transform.Find("Canvas").GetChild(0);

            TextMeshProUGUI text = textObj.GetComponent<TextMeshProUGUI>();

            if (dir == Road.DIR.DIR_IN)
            {
                text.color = Color.yellow;
            }else
            {
                text.color = Color.red;
                textObj.gameObject.SetActive(false);
            }

            //transform.GetChild(0).rotation = Quaternion.Euler(0, transform.GetChild(0).rotation.y+ angleInDegrees, 0);
            roadCnt++;
        }
    }
}
