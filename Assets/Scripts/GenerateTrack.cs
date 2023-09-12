using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrack : MonoBehaviour
{
    public static GenerateTrack Instance { get { return instance; } }//private set; }//�ǂݍ��݁iget�j�����̏ꍇ�Afunc{get;} = var���\

    private static GenerateTrack instance = null;

    public GameObject trainPrefab;

    private GameObject[] roads;

    private List<StraightRoad> roadsComponent, comeRoadsComponent;

    private void Awake()
    {

    }

    void Start()
    {

        roadsComponent = new List<StraightRoad>();

        comeRoadsComponent = new List<StraightRoad>();

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

        GetComeRoad();

        int comeRoadCnt = Random.Range(0, comeRoadsComponent.Count - 1);//�����p�x����

        Create(comeRoadCnt);
    }

    private void GetComeRoad()
    {
        foreach (StraightRoad roadCom in roadsComponent)
        {
            if(roadCom.dir == Road.DIR.DIR_IN)
            {
                comeRoadsComponent.Add(roadCom);
            }
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
