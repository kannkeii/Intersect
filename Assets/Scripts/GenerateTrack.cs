using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrack : MonoBehaviour
{
    public static GenerateTrack Instance { get { return instance; } }//private set; }//�ǂݍ��݁iget�j�����̏ꍇ�Afunc{get;} = var���\

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
        
        float radius = transform.localScale.x;//�~�̔��a
        Vector2 center = new Vector2(transform.position.x, transform.position.z);//transform.position;//�~�̒��S
        int numPoints = 3;//�擾���������W�̐�
        float minAngle = 20f;//�_�Ɠ_�̍ŏ��Ԋu(�p�x)

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
