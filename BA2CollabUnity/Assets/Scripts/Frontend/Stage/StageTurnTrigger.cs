using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class StageTurnTrigger : MonoBehaviour
{
    public enum TurnDirection
    {
        clockWise,
        counterClockWise,
        noTurn
    }
    
    public enum RoomToLoad
    {
        Office,
        Bedroom,
        Entrance,
        LivingDiningRoom,
        ScreeningRoom,
        VideoPrep,
        Garden,
        Conservatory,
    }

    public TurnDirection turnDirection;
    public RoomToLoad roomToLoad;
    public RoomToLoad theRoomThisDoorBelong;
    private DoorAdjuster doorAdjuster;

    private void Awake()
    {
        doorAdjuster = gameObject.GetComponentInParent<DoorAdjuster>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnDoorInteract();
        }
    }
    public void TurnEnvironmentClockWise()
    {
        //AdjustAllDoorsOnTurn();
        Player.instance.SetCanMove(false);
        Player.instance.transform.parent = Environment.instance.stageGround.transform;
        Environment.instance.canTurnStage = false;
        Environment.instance.turningEnviroment.transform.DORotate(Environment.instance.turningEnviroment.transform.rotation.eulerAngles + new Vector3(0, 180, 0), Environment.instance.turnDuration).SetEase(Environment.instance.turnEase).OnComplete(EndOfTurn);
    }

    public void TurnEnvironmentCounterClockWise()
    {
        //AdjustAllDoorsOnTurn();
        Player.instance.SetCanMove(false);
        Player.instance.transform.parent = Environment.instance.stageGround.transform;
        Environment.instance.canTurnStage = false;
        Environment.instance.turningEnviroment.transform.DORotate(Environment.instance.turningEnviroment.transform.rotation.eulerAngles + new Vector3(0, -180, 0), Environment.instance.turnDuration).SetEase(Environment.instance.turnEase).OnComplete(EndOfTurn);
    }
    

    void EndOfTurn()
    {
        Player.instance.SetCanMove(true);
        Player.instance.transform.parent = null;
        Environment.instance.canTurnStage = true;
        UnloadRoom();
        gameObject.GetComponent<Collider>().enabled = true;
    }

    void OnDoorInteract()
    {
        gameObject.GetComponent<Collider>().enabled = false;
        switch (turnDirection)
        {
            case TurnDirection.clockWise:
                //doorAdjuster.AdjustDoors();
                // player is idle animation
                Player.instance.animator.SetBool("isMoving",false);
                // call stage animation 
                TurnEnvironmentClockWise();
                break;
            case TurnDirection.counterClockWise:
                //doorAdjuster.AdjustDoors();
                // player is idle animation
                Player.instance.animator.SetBool("isMoving",false);
                // call stage animation 
                TurnEnvironmentCounterClockWise();
                break;
        }

        LoadRoom();
    }

    void LoadRoom()
    {
        switch (roomToLoad)
        {
            case RoomToLoad.Bedroom:
                Environment.instance.SetActiveBedroom(true);
                break;
            case RoomToLoad.Office:
                Environment.instance.SetActiveOffice(true);
                break;
            case RoomToLoad.Entrance:
                Environment.instance.SetActiveEntrance(true);
                break;
            case RoomToLoad.LivingDiningRoom:
                Environment.instance.SetActiveLivingDiningRoom(true);
                break;
            case RoomToLoad.ScreeningRoom:
                // scene go dark and come back
                UIManager.instance.MainMenuUI.mainMenuPanel.SetActive(true);
                UIManager.instance.MainMenuUI.mainMenuPanel.gameObject.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1).OnComplete(
                    () =>
                    {
                        Environment.instance.SetActiveScreeningRoom(true);
                        Player.instance.SetCanMove(false);
                        Player.instance.animator.SetBool("isMoving",false);
                        Environment.instance.canTurnStage = false;
                        
                        // teleport player
                        UIManager.instance.MainMenuUI.FadeToTransparent(1);
                        //add delay here and reset properties and unload area
                    });
                
                
                break;
            case RoomToLoad.VideoPrep:
                Environment.instance.SetActiveVideoPrep(true);
                break;
            case RoomToLoad.Garden:
                Environment.instance.SetActiveGarden(true);
                break;
            case RoomToLoad.Conservatory:
                Environment.instance.SetActiveConservatory(true);
                break;
        }
    }

    void UnloadRoom()
    {
        switch (theRoomThisDoorBelong)
        {
            case RoomToLoad.Bedroom:
                Environment.instance.SetActiveBedroom(false);
                break;
            case RoomToLoad.Office:
                Environment.instance.SetActiveOffice(false);
                break;
            case RoomToLoad.Entrance:
                Environment.instance.SetActiveEntrance(false);
                break;
            case RoomToLoad.LivingDiningRoom:
                Environment.instance.SetActiveLivingDiningRoom(false);
                break;
            case RoomToLoad.ScreeningRoom:
                Environment.instance.SetActiveScreeningRoom(false);
                break;
            case RoomToLoad.VideoPrep:
                Environment.instance.SetActiveVideoPrep(false);
                break;
            case RoomToLoad.Garden:
                Environment.instance.SetActiveGarden(false);
                break;
            case RoomToLoad.Conservatory:
                Environment.instance.SetActiveConservatory(false);
                break;
        }
    }
    
    
}
