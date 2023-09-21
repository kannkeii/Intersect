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

    //public event CreateTrainEvent OnCreateTrainEvent;


    public static GameManager Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static GameManager instance = null;

    public AddScene.GAME_MODE oldGameMode;

    

    // Start is called before the first frame update

    //Awake->OnEnable->Start

    void Start()
    {
        oldGameMode = AddScene.gameMode;
    }

    private void OnCountdownStarted()
    {
        OpingCountdown.Instance.Generate();
    }

    private void OnDestroy()
    {
        if (OpingCountdown.Instance != null)
        {
            OpingCountdown.Instance.CountdownStart -= OnCountdownStarted;
            OpingCountdown.Instance.CountdownFinished -= GameStart;
        }
    }

    public void UnsubEvents()
    {
        OnGameStart -= Turntable.Instance.Generate;

        OnGameStart -= GenerateTrack.Instance.Generate;
    }

    void GameStart()
    {
        OnGameStart += Turntable.Instance.Generate;

        OnGameStart += GenerateTrack.Instance.Generate;

        OnGameStart?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if(oldGameMode != AddScene.gameMode)
        {
            if (AddScene.gameMode == AddScene.GAME_MODE.GAME_MODE_PLAY)
            {
                if (OpingCountdown.Instance == null) return;
                
                    OpingCountdown.Instance.CountdownStart += OnCountdownStarted;

                    OpingCountdown.Instance.CountdownFinished += GameStart;

                    OpingCountdown.Instance.Generate();

                    //OpingCountdown.Instance.OnCountdownOver += GameStart;
            }

            oldGameMode = AddScene.gameMode;
        }
    }

}
