using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            
            SceneManager.LoadScene("Scenes/MainScene");
            RestartStage();
        }
        );
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RestartStage()
    {
        DeleteDontDestroyOnLoadObjcets();
        AddScene.LoadScene("StageScene");
        //SceneManager.LoadScene("Scenes/StageScene");
    }

    void DeleteDontDestroyOnLoadObjcets()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroyOnLoad");
        Debug.Log("DeleteDontDestroyOnLoadObjcets");
        foreach (GameObject obj in objs)
        {
            Destroy(obj);
        }
    }
}
