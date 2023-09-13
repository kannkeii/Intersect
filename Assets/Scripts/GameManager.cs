using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void GameStartHandler();
    public event GameStartHandler OnGameStart;

    public static GameManager Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static GameManager instance = null;

    // Start is called before the first frame update

    //Awake->OnEnable->Start
    void Start()
    {
        OnGameStart?.Invoke();

        
    }

    void OnEnable()
    {
        OnGameStart += Turntable.Instance.Generate;
        OnGameStart += GenerateTrack.Instance.Generate;


    }

    void OnDisable()
    {
        OnGameStart -= Turntable.Instance.Generate;
        OnGameStart -= GenerateTrack.Instance.Generate;
    }





    // Update is called once per frame
    void Update()
    {
        
    }

}
