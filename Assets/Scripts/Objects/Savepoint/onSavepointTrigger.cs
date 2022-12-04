using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onSavepointTrigger : MonoBehaviour
{
    private bool isUsed;
    private MainManager mainManager;
    [SerializeField] private bool isLoader;
    private void Start()
    {
        isUsed = false;
        mainManager = GameObject.FindObjectOfType<MainManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player" && !isUsed)
        {
            isUsed = true;
            this.GetComponent<AudioSource>().volume = 0.5f;
            this.GetComponent<AudioSource>().Play();
            if (!isLoader)
            {
                mainManager.SaveData(other.transform.position);
            }
            else
            {
                mainManager.LoadNextScene();
            }
        }
    }
}
