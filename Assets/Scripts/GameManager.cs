using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameStartHandler();
    public event GameStartHandler OnGameStart;
    // Start is called before the first frame update

    //Awake->OnEnable->Start
    void Start()
    {
        OnGameStart?.Invoke();
        //TrainFactory factory = new ElectricTrainFactory();
        //Train train = factory.CreateTrain();
        //train.Drive();

        
    }

    void OnEnable()
    {
        OnGameStart += GenerateTrack.Instance.Generate;


    }

    void OnDisable()
    {
        OnGameStart -= GenerateTrack.Instance.Generate;
    }





    // Update is called once per frame
    void Update()
    {
        
    }

}
