using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpingCountdown : MonoBehaviour
{
    public static OpingCountdown Instance { get { return instance; } }//private set; }//�ǂݍ��݁iget�j�����̏ꍇ�Afunc{get;} = var���\

    private static OpingCountdown instance = null;

    public event Action CountdownStart;

    public event Action CountdownFinished;

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
            Debug.LogError("OpingCountdown�̃C���X�^���X�����s���܂����B");
        }

        countdownText = GetComponent<TextMeshProUGUI>();
        //gameObject.SetActive(false);
        //StartCountdown();

        //Generate();
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
        int countdown = 5;
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

        CountdownFinished?.Invoke();

        gameObject.SetActive(false);

        isCountingDown = false;
    }
}