using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    public static AddScene Instance { get { return instance; } }//private set; }//�ǂݍ��݁iget�j�����̏ꍇ�Afunc{get;} = var���\

    private static AddScene instance = null;


    public enum GAME_MODE
    {
        GAME_MODE_TITLE,
        GAME_MODE_PLAY,
        GAME_MODE_END
    }

    public static  GAME_MODE gameMode;

    public List<string> SceneArray;
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
            Debug.LogError("AddScene�̃C���X�^���X�����s���܂����B");
        }

        gameMode = GAME_MODE.GAME_MODE_TITLE;

        foreach (var scene in SceneArray)
        {
            LoadScene(scene);
            //SceneManager.LoadScene("Scenes/"+scene, LoadSceneMode.Additive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void LoadScene(string sceneName)
    {
        if (sceneName != null)
        {
            SceneManager.LoadSceneAsync("Scenes/" + sceneName, LoadSceneMode.Additive);


            switch(sceneName)
            {
                case "TitleScene":
                    gameMode = GAME_MODE.GAME_MODE_TITLE;
                    break;
                case "MainUIScene":
                    gameMode = GAME_MODE.GAME_MODE_PLAY;
                    break;
                default:
                    break;
            }
        }
    }
}
