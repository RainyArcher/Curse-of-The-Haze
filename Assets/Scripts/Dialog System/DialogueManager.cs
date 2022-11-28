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
    [SerializeField] private float typingSpeed;

    void Start()
    {
    }

    public void StartDialog(Dialogue dialogue)
    {
        Time.timeScale = 0;
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
        StopAllCoroutines();
        StartCoroutine(TypeText(sentence));

    }
    public void EndDialog()
    {
        Time.timeScale = 1;
        isDialogueActive = false;
        dialogueObject.SetActive(isDialogueActive);
    }

    IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }
    }
}
