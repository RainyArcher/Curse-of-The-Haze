using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class MainManager : MonoBehaviour
{
    private BackgroundMusicManager backgroundMusicManager;
    public static MainManager Instance;
    /*public float force;*/
    public int sceneIndex = 0;
    public Vector3 savedPositionData = new Vector3(67.6f, 4.5f, 107.57f);

    private void Awake()
    {
        backgroundMusicManager = GameObject.FindObjectOfType<BackgroundMusicManager>();
        Cursor.lockState = CursorLockMode.Locked;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadNextScene()
    {
        backgroundMusicManager.clipIndex++;
        backgroundMusicManager.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadScene()
    {
        backgroundMusicManager.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void SaveData(Vector3 position)
    {
        savedPositionData = position;
        Debug.Log("Saved");
    }
    public Vector3 LoadData()
    {
        return savedPositionData;
    }
}
