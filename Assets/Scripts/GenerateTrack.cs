using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateTrack : MonoBehaviour
{
    public static GenerateTrack Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static GenerateTrack instance = null;

    

    public GameObject trainPrefab;

    public GameObject[] roads;

    public List<StraightRoad> roadsComponent, comeRoadsComponent,outRoadComponent;

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

    public void GenerateLevel1()
    {
        roads = GameObject.FindGameObjectsWithTag("Road");

        foreach (GameObject roadObjcet in roads)
        {
            roadsComponent.Add(roadObjcet.GetComponent<StraightRoad>());
        }

        GetComeOutRoad();

        Create();
    }

    public void GenerateLevel2()
    {

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
        if (comeRoadsComponent.Count <= 0 || outRoadComponent.Count <= 0) return;


        int roadNum = 1;//Random.Range(0, comeRoadsComponent.Count-1);
        roadNum++;
        //Debug.Log(roadNum);
        int trainCnt = 0;
        for (int roadCnt =0;roadCnt< roadNum; roadCnt++)
        {
            if (comeRoadsComponent.Count <= 0 || outRoadComponent.Count<=0) return;

            int comeRoadCnt = Random.Range(0, comeRoadsComponent.Count - 1);

            int exitRoadCnt = Random.Range(0, outRoadComponent.Count - 1);

            Create(comeRoadCnt, exitRoadCnt, trainCnt);

            trainCnt++;

            Transform c = comeRoadsComponent[comeRoadCnt].transform.Find("Canvas");
            
            Transform comRoadObj = c.GetChild(0);
            comRoadObj.gameObject.SetActive(true);
            TextMeshProUGUI comRoadText = comRoadObj.GetComponent<TextMeshProUGUI>();
            
            comRoadText.color = Color.red;

            comeRoadsComponent.RemoveAt(comeRoadCnt);

            break;
        }
    }

    private void Create(int enterRoadCnt, int exitRoadCnt, int trainCnt)
    {
        TrainFactory trainFactory = new DieselTrainFactory();
        GameObject dieselTrain = trainFactory.CreateTrain(trainPrefab);
        dieselTrain.transform.parent = this.transform.parent;
        dieselTrain.transform.position = comeRoadsComponent[enterRoadCnt].endRoadTransform.position;
        dieselTrain.GetComponent<TrainMove>().comeRoad = comeRoadsComponent[enterRoadCnt].gameObject;
        dieselTrain.GetComponent<TrainMove>().exitRoad = outRoadComponent[exitRoadCnt].gameObject;
        dieselTrain.GetComponent<TrainMove>().endRoadTransform = outRoadComponent[exitRoadCnt].endRoadTransform;
        dieselTrain.name = trainPrefab.name + "_" + trainCnt;
        dieselTrain.GetComponent<TrainMove>().actionMode = Train.ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD;
        dieselTrain.transform.LookAt(this.transform.parent.position);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.thisLevelTrainAllNum++;
        }else
        {
            Debug.LogError("GameManagerインスタンス失敗");
        }
    }

    
}
