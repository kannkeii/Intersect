using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string Scene;
    // Start is called before the first frame update
    void Awake()
    {
        TitleAnyKeyInput.Instance.OnAnyKeyDownHandler += LoadScene;
    }

    private void OnDestroy()
    {
        TitleAnyKeyInput.Instance.OnAnyKeyDownHandler -= LoadScene;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadScene()
    {
        SceneManager.LoadSceneAsync(Scene);
    }
}
