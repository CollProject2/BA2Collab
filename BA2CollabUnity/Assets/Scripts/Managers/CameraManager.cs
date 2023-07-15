using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    // Camera movement control animations
    public static CameraManager instance;
    
    [Header("Positions")]
    [SerializeField] private Transform camEndPos;
    [SerializeField] private Transform camBeginPos;
    [SerializeField] private Transform camPosAtGarden;
    [SerializeField] private Transform camPosAtScreeningRoom;
    [SerializeField] private Transform TempPosInit;
    [SerializeField] private Transform TempPosGarden;
    
    [Header("Durations")]
    [SerializeField] private float cameraZoomDuration = 2;
    
    public bool look = false;
    public GameObject tempFocus;
    private void Awake()
    {
        //Singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        if (look)
        {
            Camera.main.transform.LookAt(tempFocus.transform);
        }
    }

    public void CameraZoomSequence()
    {
        Camera.main.transform.DOMove(camEndPos.transform.position, cameraZoomDuration);
        Camera.main.transform.DORotate(new Vector3(6.945f, 0, 0), cameraZoomDuration).OnComplete(OnCameraZoomEnd);
    }
    public void CameraZoomOutSequence()
    {
        Camera.main.transform.DOMove(camBeginPos.transform.position, cameraZoomDuration);
        Camera.main.transform.DORotate(new Vector3(-1.97f, 0, 0), cameraZoomDuration).OnComplete(() => UIManager.instance.dialogues.dialogueBox.MoveToPassivePos());
    }

    public void CameraZoomInGarden()
    {
        Camera.main.transform.DOMove(camPosAtGarden.transform.position, cameraZoomDuration);
        tempFocus.transform.DOMove(TempPosGarden.position, cameraZoomDuration);
    }
    public void CameraZoomOutFromGarden()
    {
        Camera.main.transform.DOMove(camEndPos.transform.position, cameraZoomDuration);
        tempFocus.transform.DOMove(TempPosInit.position, cameraZoomDuration);
    }

    public void CameraZoomInScreeningRoom()
    {
        Camera.main.transform.DOMove(camPosAtScreeningRoom.transform.position, cameraZoomDuration);
    }
    public void CameraZoomOutScreeningRoom()
    {
        Camera.main.transform.DOMove(camEndPos.transform.position, cameraZoomDuration);
    }


    private void OnCameraZoomEnd()
    {
        look = true;
        UIManager.instance.MainMenuUI.canPause = true;
        Player.instance.SetCanMove(true);
        LightManager.instance.OpenFrontStageLights(false);
        UIManager.instance.dialogues.dialogueBox.MoveToActivePos();
        Invoke("StartFirstDialogue", 1);
    }
    
    void StartFirstDialogue()
    {
        UIManager.instance.dialogues.StartDialogue("OnGameStartCall");
    }
}
