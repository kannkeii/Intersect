using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TitleAnyKeyInput : MonoBehaviour
{
    public delegate void AnyKeyDownHandler();

    public event AnyKeyDownHandler OnAnyKeyDownHandler;

    //public GameObject WindowBG;

    //public List<GameObject> CloseUi;

    public static TitleAnyKeyInput Instance { get { return instance; } }//private set; }//読み込み（get）だけの場合、func{get;} = varが可能

    private static TitleAnyKeyInput instance = null;

    private bool onAnyKeyDownIsHandled = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }else
        {
            Destroy(gameObject);
            Debug.LogError("TitleAnyKeyInputのインスタンスが失敗しました。");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetKey//检测键盘输入
        //Input.GetMouseButton//检测鼠标输入
        //Input.GetAxis//检测在Input Manager中定义的虚拟轴的值
        //Input.GetButton//检测在Input Manager中定义的虚拟按钮是否被按下

        if (MouseAnyKeyDown() &&
            !onAnyKeyDownIsHandled)
        {
            OnAnyKeyDownHandler?.Invoke();

            //CloseAllUi();

            onAnyKeyDownIsHandled = true;
        }

    }

    bool MouseAnyKeyDown()
    {
        if (Input.anyKeyDown)
        {
            if (EventSystem.current.currentSelectedGameObject != null) return false;

            //if (WindowBG.gameObject.activeSelf)
                //return false;

            return true;
        }

        return false;
    }

    //void CloseAllUi()
    //{
    //    foreach(var item in CloseUi)
    //    {
    //        item.SetActive(false);
    //    }
    //}
}
