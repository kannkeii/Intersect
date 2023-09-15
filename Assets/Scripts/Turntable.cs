using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turntable : MonoBehaviour
{
    public static Vector3 LocalScale {get {return localScale;} }

    public int roadNum;//取得したい座標の数

    static Vector3 localScale = default;

    public GameObject RoadObject;

    public static Turntable Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static Turntable instance = null;

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("Turntableのインスタンスが失敗しました。");
        }
    }

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
    public void Generate()
    {
        
        float radius = transform.localScale.x;//円の半径
        Vector2 center = new Vector2(transform.position.x, transform.position.z);//transform.position;//円の中心
        
        if (roadNum <= 0) roadNum = 2;
        else if (roadNum % 2 != 0) roadNum++;
        float minAngle = 20f;//点と点の最小間隔(角度)

        List<Vector2> points = CirclePointsGenerator.GeneratePoints(radius, center, roadNum, minAngle);

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

            //transform.GetChild(0).rotation = Quaternion.Euler(0, transform.GetChild(0).rotation.y+ angleInDegrees, 0);
            roadCnt++;
        }
    }
}
