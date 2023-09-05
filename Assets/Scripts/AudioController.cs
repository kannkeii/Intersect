using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get { return instance; } set { instance = value; } }

    private static AudioController instance = null;

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            Debug.LogError("AudioControllerのインスタンスが失敗しました。");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool GetStatus()
    {
        return audioSource.mute;
    }

    public void Change()
    {
        audioSource.mute = !audioSource.mute;
    }
}
