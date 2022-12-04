using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    /*public float force;*/
    public int sceneIndex = 0;
    public Vector3 savedPositionData = new Vector3(67.6f, 4.5f, 107.57f);
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReloadScene()
    {
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
