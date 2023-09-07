using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrack : MonoBehaviour
{
    public static GenerateTrack Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static GenerateTrack instance = null;

    public GameObject Road;
    private void Awake()
    {

    }

    void Start()
    {
        
    }

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("GenerateTrackのインスタンスが失敗しました。");
        }
    }

    void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        
        float radius = transform.localScale.x;//円の半径
        Vector2 center = new Vector2(transform.position.x, transform.position.z);//transform.position;//円の中心
        int numPoints = 3;//取得したい座標の数
        float minAngle = 20f;//点と点の最小間隔(角度)

        List<Vector2> points = CirclePointsGenerator.GeneratePoints(radius, center, numPoints, minAngle);

        foreach(var point in points)
        {
            float angleInDegrees = CirclePointsGenerator.GetPointAngle(center, point);

            Transform transform = Instantiate(Road).transform;
            transform.parent = this.transform;
            transform.position = new Vector3(point.x,0 , point.y);
            transform.eulerAngles = new Vector3(0, angleInDegrees, 0);
        }
    }



    //private List<Vector3> GetTrackStartPosition()
    //{

    //}
}
