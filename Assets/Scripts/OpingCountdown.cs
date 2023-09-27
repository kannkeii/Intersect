using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpingCountdown : MonoBehaviour
{
    public static OpingCountdown Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static OpingCountdown instance = null;

    public event Action CountdownStart;

    //public event Action CountdownFinished;

    private bool isCountingDown = false;

    private TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("OpingCountdownのインスタンスが失敗しました。");
        }

        countdownText = GetComponent<TextMeshProUGUI>();
        //gameObject.SetActive(false);
        //StartCountdown();

        //Generate();
    }

    void OnDestroy()
    {
        CountdownStart = null;
    }

    void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generate()
    {
        //if ()
        {
            AudioController.Instance.PlayMusic(3);
        }
        gameObject.SetActive(true);
        StartCountdown();
    }

    public void StartCountdown()
    {
        if (!isCountingDown)
        {
            StartCoroutine(CountdownCoroutine());
            isCountingDown = true;
        }
    }

    IEnumerator CountdownCoroutine()
    {
        int countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            countdown--;
            yield return new WaitForSeconds(1);
        }

        countdownText.text = "GameStart!";
        countdownText.fontSize = 96;

        CountdownStart?.Invoke();

        yield return new WaitForSeconds(1);

        GameManager.Instance.TriggerOnCountdownFinished();

        gameObject.SetActive(false);

        isCountingDown = false;
    }
}
