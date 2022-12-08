using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questBar;
    [SerializeField] private TextMeshProUGUI questTitleText;
    [SerializeField] private TextMeshProUGUI questDescriptionText;
    [SerializeField] private TextMeshProUGUI questStagesText;
    private CharacterController player;
    public List<Quest> villageQuests;
    public int currentStages;
    public int currentQuestIndex;
    void Start()
    {
        player = GameObject.FindObjectOfType<CharacterController>();
        currentQuestIndex = 0;
        currentStages = 0;
    }
    void Update()
    {
        Quest currentQuest = villageQuests[currentQuestIndex];
        questTitleText.text = currentQuest.title;
        questDescriptionText.text = currentQuest.description;
        questStagesText.text = $"{currentStages}/{currentQuest.stages}";
        if (currentQuest.stages <= currentStages)
        {
            if (currentQuest.barriers != null)
            {
                foreach (GameObject barrier in currentQuest.barriers)
                {
                    barrier.SetActive(false);
                }
            }
            currentStages = 0;
            currentQuestIndex++;
            if (currentQuestIndex >= villageQuests.Count)
            {
                Hide();
            }
        }
    }
    void Hide()
    {
        questBar.SetActive(false);
        gameObject.SetActive(false);
    }
}
