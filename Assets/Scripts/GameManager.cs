using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameStartHandler();
    public event GameStartHandler OnGameStart;

    public delegate void CreateTrainEvent();

    public event CreateTrainEvent OnCreateTrainEvent;

    public static GameManager Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static GameManager instance = null;


    //public Dictionary<string, bool> handlerStatus;

    // Start is called before the first frame update

    //Awake->OnEnable->Start

    void Start()
    {

        //handlerStatus = new Dictionary<string, bool>();

        //handlerStatus["Turntable::Generate"] = false;
        //handlerStatus["GenerateTrack::Generate"] = false;

        OnGameStart += Turntable.Instance.Generate;
        OnGameStart += GenerateTrack.Instance.Generate;
        //OnGameStart += OpingCountdown.Instance.StartCountdown;
        OnGameStart?.Invoke();
        //StartCoroutine(BegainStatus());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //IEnumerator BegainStatus()
    //{
    //    while (true)
    //    {
    //        if(handlerStatus.All(status=> status.Value == true))
    //        {
                
    //            break;
    //        }

    //        yield return null;
    //    }



    //    yield break;
    //}

}
