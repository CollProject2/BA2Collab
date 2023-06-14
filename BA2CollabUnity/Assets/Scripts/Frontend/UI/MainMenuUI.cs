using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // the UI panel appears instantly now, in the future we will have a sequence
    // zoom in, open curtains, open related buttons....
    
    //Properties
    public bool isDisplayed { get; private set; }

    [Header("Objects")] 
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject title;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject exitButton;
    
    [Header("Positions")]
    [SerializeField] private Transform camEndPos;
    [SerializeField] private Transform titleEndPos;
    [SerializeField] private Transform startButtonEndPos;
    [SerializeField] private Transform exitButtonEndPos;
    
    [Header("Durations")]
    [SerializeField] private float cameraZoomDuration = 2;
    [SerializeField] private float titleMoveDuration = 4;
    [SerializeField] private float startButtonMoveDuration = 2;
    [SerializeField] private float exitButtonMoveDuration = 2;
    
    [Header("Curves")] 
    [SerializeField] private AnimationCurve titleMoveCurve;
    

    private void Start()
    {
        DisplayMainMenuUI();
        MainMenuSequence();
        
    }

    void DisplayMainMenuUI()
    {
        isDisplayed = true;
        mainMenuPanel.SetActive(true);
    }
    private void CameraZoomSequence()
    {
        Camera.main.transform.DOMove(camEndPos.transform.position, cameraZoomDuration);
        Camera.main.transform.DORotate(new Vector3(0, 0, 0), cameraZoomDuration);
    }

    void MainMenuSequence()
    {
        FadeToTransparent();
        MoveTitleUp();
    }

    void FadeToTransparent()
    {
        mainMenuPanel.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 4);
    }

    void MoveTitleUp()
    {
        title.transform.DOMove(titleEndPos.position, titleMoveDuration).SetEase(titleMoveCurve).OnComplete(MoveMenuButtonsIn);
    }

    void MoveMenuButtonsIn()
    {
        startButton.transform.DOMove(startButtonEndPos.position, startButtonMoveDuration);
        exitButton.transform.DOMove(exitButtonEndPos.position, exitButtonMoveDuration);
    }

    //Buttons
    public void StartGameButton()
    {
        mainMenuPanel.SetActive(false);
        CameraZoomSequence();
        isDisplayed = false;
    }
    public void ExitGameButton()
    {
        Application.Quit();
    }
}
