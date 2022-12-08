using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onSavepointTrigger : MonoBehaviour
{
    private bool isUsed;
    private MainManager mainManager;
    private QuestManager questManager;
    [SerializeField] private bool isLoader;
    private void Start()
    {
        isUsed = false;
        mainManager = GameObject.FindObjectOfType<MainManager>();
        questManager= GameObject.FindObjectOfType<QuestManager>();
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
                mainManager.savedPositionData = other.transform.position;
                mainManager.questIndex = questManager.currentQuestIndex;
            }
            else
            {
                mainManager.LoadNextScene();
            }
        }
    }
}
