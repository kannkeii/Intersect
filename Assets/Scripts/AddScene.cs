using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    public List<string> SceneArray;
    // Start is called before the first frame update
    void Start()
    {
        foreach(var scene in SceneArray)
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
        Debug.Log("LoadScene");
        if (sceneName != null)
            SceneManager.LoadSceneAsync("Scenes/" + sceneName, LoadSceneMode.Additive);
    }
}
