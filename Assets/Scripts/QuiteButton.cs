using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuiteButton : MonoBehaviour
{
    private bool lastMuteValue;
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();

        text.text = "♪";


        GetComponent<Button>().onClick.AddListener(() =>
        {
            AudioController.Instance.Change();
            if (AudioController.Instance.GetStatus() != lastMuteValue)
            {
                if(AudioController.Instance.GetStatus() == true)
                    text.text = '\u0336'+"♪" + '\u0336';
                else
                    text.text = "♪";

                lastMuteValue = AudioController.Instance.GetStatus();
            }
        }
        );

        lastMuteValue = AudioController.Instance.GetStatus();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
