using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainGauge : MonoBehaviour
{
    public float decreaseTime = 5f;

    private TrainMove trainMove;
    private Transform gaugeObj;
    private Image image;
    private Camera mainCamera;
    private bool hasStartedDecreasing = false;

    private float decreaseSpeed;

    void Start()
    {
        trainMove = GetComponent<TrainMove>();
        trainMove.OnWaitAtRoadEnd += StartDecreasingGauge;
        //trainMove.OnNotWaitAtRoadEnd += ResetGauge;
        gaugeObj = transform.Find("Canvas").GetChild(0);
        image = gaugeObj.GetComponent<Image>();
        mainCamera = Camera.main;
    }

    void OnDisable()
    {
        trainMove.OnWaitAtRoadEnd -= StartDecreasingGauge;
        //trainMove.OnNotWaitAtRoadEnd -= ResetGauge;
    }

    void Update()
    {
        gaugeObj.transform.LookAt(mainCamera.transform.position, Vector3.forward);
    }

    void StartDecreasingGauge()
    {
        if (hasStartedDecreasing == true) return;

        decreaseSpeed = 1f / decreaseTime;

        hasStartedDecreasing = true;
        StartCoroutine(DecreaseGauge());

    }

    System.Collections.IEnumerator DecreaseGauge()
    {
        while (image.fillAmount > 0)
        {
            image.fillAmount -= decreaseSpeed * Time.deltaTime;
            yield return null;
        }

        Debug.Log("GameOver");
    }

    //void ResetGauge()
    //{
    //    StopCoroutine(DecreaseGauge());
    //    image.fillAmount = 1f;
    //    hasStartedDecreasing = false;

       
    //    // gaugeObj.SetActive(false);
    //}
}
