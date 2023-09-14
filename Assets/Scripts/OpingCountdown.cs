using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpingCountdown : MonoBehaviour
{
    public static OpingCountdown Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static OpingCountdown instance = null;

    private TextMeshProUGUI countdownText;

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponent<TextMeshProUGUI>();
        //GameManager.Instance.OnGameStart += StartCountdown;
        StartCountdown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCountdown()
    {
        StartCoroutine(CountdownCoroutine());
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

        yield return new WaitForSeconds(1);

        gameObject.SetActive(false);
    }
}
