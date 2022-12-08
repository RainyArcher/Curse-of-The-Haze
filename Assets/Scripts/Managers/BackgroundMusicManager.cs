using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusicManager : MonoBehaviour
{
    [SerializeField] private List<AudioClip> backgroundMusicClipList;
    [SerializeField] private AudioSource musicAudio;
    private static BackgroundMusicManager Instance;
    public int clipIndex;
    private void Awake()
    {
        clipIndex = 0;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Play();
    }
    public void Play()
    {
        musicAudio.clip = backgroundMusicClipList[clipIndex];
        musicAudio.Play();
    }
    public void Stop()
    {
        musicAudio.Stop();
    }
}
