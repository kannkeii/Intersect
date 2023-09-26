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

    public static event Action OnCountdownFinished;

    //public event CreateTrainEvent OnCreateTrainEvent;

    public static GameManager Instance { get { return instance; } }

    private static GameManager instance = null;

    public AddScene.GAME_MODE oldGameMode;

    public int nowLevelIndex = 0;//ターンIndex

    public int thisLevelTrainAllNum = 0;//現在のターンの全列車数

    public int thisLevelTrainNum = 0;//現在のターンの列車数


    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        oldGameMode = AddScene.gameMode;

        GameStart();
    }

    private void OnCountdownStarted()
    {
        OpingCountdown.Instance.Generate();
    }

    private void OnDestroy()
    {
        //if (OpingCountdown.Instance != null)
        {
            //OpingCountdown.Instance.CountdownStart -= OnCountdownStarted;
            //OpingCountdown.Instance.CountdownFinished -= GameStart;
        }
    }

    public void UnsubEvents()
    {
        OnGameStart = null;
    }

    void GameStart()
    {
        UnsubEvents();

        OnGameStart += Turntable.Instance.GenerateLevel1;
        OnGameStart += GenerateTrack.Instance.GenerateLevel1;

        StartCoroutine(WaitForOpingCountdown());
    }

    IEnumerator WaitForOpingCountdown()
    {
        while (OpingCountdown.Instance == null)
        {
            yield return null;
        }

        
        //OpingCountdown.Instance.CountdownStart += OnCountdownStarted;
        //OpingCountdown.Instance.CountdownFinished += () =>
        //{
        //    OnGameStart?.Invoke();
        //};
        OpingCountdown.Instance.Generate();

        OnCountdownFinished = null;

        OnCountdownFinished += () =>
        {
            AudioController.Instance.PlayMusic(1);
            OnGameStart?.Invoke();
        };

    }

    void Update()
    {

    }


    public void GameOver()
    {

        Turntable.Instance.DestoryAllRoad();

        GenerateTrack.Instance.roadsComponent.Clear();

        GenerateTrack.Instance.comeRoadsComponent.Clear();
        GenerateTrack.Instance.outRoadComponent.Clear();

        GameStart();
    }

    public void TriggerOnCountdownFinished()
    {
        OnCountdownFinished?.Invoke();
    }
}
