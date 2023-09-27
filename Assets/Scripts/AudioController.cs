using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get { return instance; } set { instance = value; } }

    public static  List<AudioClip> musicList = new List<AudioClip>();

    private static AudioController instance = null;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            audioSource.Stop();
            Destroy(gameObject);
            Debug.LogError("AudioControllerのインスタンスが失敗しました。");
        }

        //DontDestroyOnLoad(this.gameObject);
        //this.gameObject.tag = "DontDestroyOnLoad";
    }

    // Start is called before the first frame update
    void Start()
    {
        if (musicList.Count <= 0)
        {
            StartCoroutine(LoadAudioClips());
        }
        //if (musicList.Count <= 0)
        //{
        //    musicList.Add(Resources.Load<AudioClip>("Music/放課後はお菓子でもつまみながら的なBGM"));
        //    musicList.Add(Resources.Load<AudioClip>("Music/昼下がりのお遊戯的なBGM"));
        //    musicList.Add(Resources.Load<AudioClip>("Music/thank-you-21604"));
        //    musicList.Add(Resources.Load<AudioClip>("Music/countdown-sound-effect-8-bit-151797"));
        //}
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

    public void PlayMusic(int musicCnt)
    {
        audioSource.clip = musicList[musicCnt];
        audioSource.Play();
    }

    private IEnumerator LoadAudioClips()
    {
        musicList.Add(Resources.Load<AudioClip>("Music/放課後はお菓子でもつまみながら的なBGM"));
        yield return null;
        musicList.Add(Resources.Load<AudioClip>("Music/昼下がりのお遊戯的なBGM"));
        yield return null;
        musicList.Add(Resources.Load<AudioClip>("Music/thank-you-21604"));
        yield return null;
        musicList.Add(Resources.Load<AudioClip>("Music/countdown-sound-effect-8-bit-151797"));
        yield return null;
    }
}
