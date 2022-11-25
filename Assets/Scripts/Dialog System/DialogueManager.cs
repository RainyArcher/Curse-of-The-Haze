using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueObject;
    public Queue<string> sentences = new Queue<string>();
    public bool isDialogueActive = false;

    void Start()
    {
    }

    public void StartDialog(Dialogue dialogue)
    {
        isDialogueActive = true;
        dialogueObject.SetActive(isDialogueActive);
        sentences.Clear();
        nameText.text = dialogue.name;
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        
    }
    public void EndDialog()
    {
        isDialogueActive= false;
        dialogueObject.SetActive(isDialogueActive);
    }
}
