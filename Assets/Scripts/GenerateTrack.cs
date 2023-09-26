using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GenerateTrack : MonoBehaviour
{
    public static GenerateTrack Instance { get { return instance; } }//private set; }//�ǂݍ��݁iget�j�����̏ꍇ�Afunc{get;} = var���\

    private static GenerateTrack instance = null;

    

    public GameObject trainPrefab;

    private GameObject[] roads;

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
            Debug.LogError("GenerateTrack�̃C���X�^���X�����s���܂����B");
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

        
        int roadNum = Random.Range(0, comeRoadsComponent.Count-1);
        roadNum++;
        Debug.Log(roadNum);
        int trainCnt = 0;
        for (int roadCnt =0;roadCnt< roadNum; roadCnt++)
        {
            if (comeRoadsComponent.Count <= 0 || outRoadComponent.Count<=0) return;

            int comeRoadCnt = Random.Range(0, comeRoadsComponent.Count - 1);

            int exitRoadCnt = Random.Range(0, outRoadComponent.Count - 1);

            Create(comeRoadCnt, exitRoadCnt, trainCnt);

            trainCnt++;


            Transform comRoadObj = comeRoadsComponent[comeRoadCnt].transform.Find("Canvas").GetChild(0);

            TextMeshProUGUI comRoadText = comRoadObj.GetComponent<TextMeshProUGUI>();

            comRoadText.color = Color.red;

            comeRoadsComponent.RemoveAt(comeRoadCnt);

        }
    }

    private void Create(int enterRoadCnt, int exitRoadCnt, int trainCnt)
    {
        TrainFactory trainFactory = new DieselTrainFactory();
        GameObject dieselTrain = trainFactory.CreateTrain(trainPrefab);
        dieselTrain.transform.parent = this.transform.parent;
        dieselTrain.transform.position = comeRoadsComponent[enterRoadCnt].endRoadTransform.position;
        dieselTrain.GetComponent<TrainMove>().exitRoad = outRoadComponent[exitRoadCnt].gameObject;
        dieselTrain.GetComponent<TrainMove>().endRoadTransform = outRoadComponent[exitRoadCnt].endRoadTransform;
        dieselTrain.name = trainPrefab.name + "_" + trainCnt;
        dieselTrain.GetComponent<TrainMove>().actionMode = Train.ACTION_MODE.ACTION_MODE_WAIT_AT_ROAD;
        dieselTrain.transform.LookAt(this.transform.parent.position);
    }
}
