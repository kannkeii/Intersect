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

            //SceneManager.LoadScene("Scenes/MainScene");
            RestartStage();
            //OpingCountdown.Instance.Generate();
        }
        );
    }

    // Update is called once per frame
    void Update()
    {

    }

    void RestartStage()
    {

        //GameManager.Instance?.UnsubEvents();
        //DeleteDontDestroyOnLoadObjcets();
        //SceneManager.UnloadSceneAsync("StageScene");
        //AddScene.LoadScene("MainScene");
        //SceneManager.LoadScene("Scenes/StageScene");
;
        SceneManager.LoadScene("Scenes/MainScene");
        AddScene.Instance.SceneArray.Clear();
        AddScene.Instance.SceneArray.Add("StageScene");
        //GameManager.Instance.oldGameMode = AddScene.GAME_MODE.GAME_MODE_TITLE;


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
