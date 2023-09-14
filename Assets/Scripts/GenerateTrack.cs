using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrack : MonoBehaviour
{
    public static GenerateTrack Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static GenerateTrack instance = null;

    

    public GameObject trainPrefab;

    private GameObject[] roads;

    private List<StraightRoad> roadsComponent, comeRoadsComponent,outRoadComponent;

    private void Awake()
    {

    }

    void Start()
    {

        roadsComponent = new List<StraightRoad>();

        comeRoadsComponent = new List<StraightRoad>();

        outRoadComponent = new List<StraightRoad>();

        
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
        roads = GameObject.FindGameObjectsWithTag("Road");

        foreach (GameObject roadObjcet in roads)
        {
            roadsComponent.Add(roadObjcet.GetComponent<StraightRoad>());
        }

        GetComeOutRoad();

        Create();

        //GameManager.Instance.handlerStatus["GenerateTrack::Generate"] = true;
    }

    private void GetComeOutRoad()
    {
        foreach (StraightRoad roadCom in roadsComponent)
        {
            if(roadCom.dir == Road.DIR.DIR_IN)
            {
                comeRoadsComponent.Add(roadCom);
            }else if(roadCom.dir == Road.DIR.DIR_OUT)
            {
                outRoadComponent.Add(roadCom);
            }
        }
    }

    void Create()
    {
        if (comeRoadsComponent.Count <= 0) return;

        
        int roadNum = Random.Range(0, comeRoadsComponent.Count-1);
        roadNum++;
        Debug.Log(roadNum);
        for (int roadCnt =0;roadCnt< roadNum; roadCnt++)
        {
            if (comeRoadsComponent.Count <= 0) return;

            int comeRoadCnt = Random.Range(0, comeRoadsComponent.Count - 1);

            Create(comeRoadCnt);

            comeRoadsComponent.RemoveAt(comeRoadCnt);

        }
    }

    private void Create(int roadCnt)
    {
        TrainFactory trainFactory = new DieselTrainFactory();
        GameObject dieselTrain = trainFactory.CreateTrain(trainPrefab);
        dieselTrain.transform.parent = this.transform.parent;
        dieselTrain.transform.position = comeRoadsComponent[roadCnt].endRoadTransform.position;
        dieselTrain.transform.LookAt(this.transform.parent.position);
    }
}
