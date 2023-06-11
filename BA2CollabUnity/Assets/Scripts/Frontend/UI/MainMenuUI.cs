using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    // the UI panel appears instantly now, in the future we will have a sequence
    // zoom in, open curtains, open related buttons....
    
    //Properties
    [SerializeField] private bool isDisplayed;
    [SerializeField] private GameObject mainMenuPanel, camEndPos;
    [SerializeField] private float cameraZoomDuration = 2;
    

    private void Start()
    {
        DisplayMainMenuUI();
    }

    void DisplayMainMenuUI()
    {
        isDisplayed = true;
        mainMenuPanel.SetActive(true);
    }

    public void StartGameButton()
    {
        mainMenuPanel.SetActive(false);
        CameraZoomSequence();
        isDisplayed = false;
    }

    private void CameraZoomSequence()
    {
        Camera.main.transform.DOMove(camEndPos.transform.position, cameraZoomDuration);
        Camera.main.transform.DORotate(new Vector3(0, 0, 0), cameraZoomDuration);
    }
    
    

    public void ExitGameButton()
    {
        Application.Quit();
    }
}
