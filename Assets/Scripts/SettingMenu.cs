using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    public Button MenuOnOffButton;
    // Start is called before the first frame update
    private RectTransform windowRectTransform;
    private RectTransform onOffButtonRectTransform;
    private TextMeshProUGUI onOffButtontext;
    private bool showMenuFlag = default;
    private Vector3 oldPosition;
    void Start()
    {
        windowRectTransform = GetComponent<RectTransform>();
        onOffButtonRectTransform = MenuOnOffButton.GetComponent<RectTransform>();
        onOffButtontext = MenuOnOffButton.GetComponentInChildren<TextMeshProUGUI>();
        oldPosition = transform.localPosition;
        showMenuFlag = false;
        MenuOnOffButton.onClick.AddListener(MenuOnOff);
        //MenuOnOff();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MenuOnOff()
    {
        if (showMenuFlag)
        {
            transform.localPosition = oldPosition;
            showMenuFlag = false;
            onOffButtontext.text = "Å©";
        }
        else
        {
            transform.localPosition = new Vector3(
                transform.localPosition.x - windowRectTransform.rect.width+ onOffButtonRectTransform.rect.width,
                transform.localPosition.y,
                transform.localPosition.z
                );

            onOffButtontext.text = "Å®";
            showMenuFlag = true;
        }
    }
}
