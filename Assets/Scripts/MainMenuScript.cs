using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    private MainManager mainManager;
    private int selectedButtonIndex;
    private int buttonCount = 2;
    private bool isGameStarted;
    [SerializeField] private List<Button> buttons = new List<Button>();
    [SerializeField] private List<Sprite> buttonImages = new List<Sprite>();

    private void Start()
    {
        isGameStarted= false;
        selectedButtonIndex = 0;
        SelectButton();
        mainManager = GameObject.FindObjectOfType<MainManager>();
    }
    public void OnStartButtonClicked()
    {
        if (!isGameStarted)
        {
            isGameStarted = true;
            SetButtonSprite(2);
            mainManager.LoadNextScene();
        }
    }
    public void OnQuitButtonClicked()
    {
        if (!isGameStarted)
        {
            SetButtonSprite(2);
            mainManager.Quit();
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            DeselectButton();
            if (selectedButtonIndex < buttonCount - 1)
                selectedButtonIndex++;
            else
                selectedButtonIndex = 0;
            SelectButton();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            DeselectButton();
            if (selectedButtonIndex > 0)
                selectedButtonIndex--;
            else
                selectedButtonIndex = buttonCount - 1;
            SelectButton();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            buttons[selectedButtonIndex].onClick.Invoke();
        }
    }
    private void SelectButton()
    {
        SetButtonSprite(1);
    }
    private void DeselectButton()
    {
        SetButtonSprite(0);
    }
    private void SetButtonSprite(int index)
    {
        buttons[selectedButtonIndex].GetComponent<Image>().sprite = buttonImages[index];
    }
}
