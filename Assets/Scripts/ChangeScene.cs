using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string Scene;
    // Start is called before the first frame update
    void Start()
    {
        TitleAnyKeyInput.Instance.OnAnyKeyDownHandler += () => { SceneManager.LoadScene(Scene); };//LoadSceneAsync
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
