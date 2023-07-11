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
        PartiallyUnloadRoom();
        Player.instance.SetCanMove(false);
        Player.instance.transform.parent = Environment.instance.stageGround.transform;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.roomChange, new Vector3(0,0,0));
        Environment.instance.canTurnStage = false;
        Environment.instance.turningEnviroment.transform.DORotate(Environment.instance.turningEnviroment.transform.rotation.eulerAngles + new Vector3(0, 180, 0), Environment.instance.turnDuration).SetEase(Environment.instance.turnEase).OnComplete(EndOfTurn);
    }

    public void TurnEnvironmentCounterClockWise()
    {
        //AdjustAllDoorsOnTurn();
        PartiallyUnloadRoom();
        Player.instance.SetCanMove(false);
        Player.instance.transform.parent = Environment.instance.stageGround.transform;
        AudioManager.instance.PlayOneShot(FMODEvents.instance.roomChange, new Vector3(0,0,0));
        Environment.instance.canTurnStage = false;
        Environment.instance.turningEnviroment.transform.DORotate(Environment.instance.turningEnviroment.transform.rotation.eulerAngles + new Vector3(0, -180, 0), Environment.instance.turnDuration).SetEase(Environment.instance.turnEase).OnComplete(EndOfTurn);
    }
    

    void EndOfTurn()
    {
        Player.instance.SetCanMove(true);
        Player.instance.transform.parent = null;
        Environment.instance.canTurnStage = true;
        UnloadRoom();
        Invoke("ColliderDelay",0.1f);
    }

    void ColliderDelay()
    {
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
                TpToBedroom();
                break;
            case RoomToLoad.Office:
                Environment.instance.SetActiveOffice(true);
                Environment.instance.DeactivateBedroomToEntrance();
                break;
            case RoomToLoad.Entrance:
                TpToEntrance();
                Environment.instance.DeactivateLivingToScrDoor();
                break;
            case RoomToLoad.LivingDiningRoom:
                // works only if the door is set to no turn -> from screening room to living room transition
                TpToLivingRoom();
                Environment.instance.DeactivateEntranceToBedRoomDoor();
                break;
            case RoomToLoad.ScreeningRoom:
                // from livingroom to screening room, no turn
                TpToScreeningRoom();
                VideoRoomToScreeningRoomExeption();
                break;
            case RoomToLoad.VideoPrep:
                Environment.instance.SetActiveVideoPrep(true);
                StartCoroutine(DelayForLoadingGardenAndConservatory());
                Environment.instance.DeactivateScreenToLivDoor();
                break;
            case RoomToLoad.Garden:
                break;
            case RoomToLoad.Conservatory:
                break;
        }
    }
    void PartiallyUnloadRoom()
    {
        switch (theRoomThisDoorBelong)
        {
            case RoomToLoad.Bedroom:
                Environment.instance.partialLoader_BedRoom.PartiallyUnload();
                break;
            case RoomToLoad.Office:
                Environment.instance.partialLoader_Office.PartiallyUnload();
                break;
            case RoomToLoad.Entrance:
                break;
            case RoomToLoad.LivingDiningRoom:
                Environment.instance.partialLoader_LivingRoom.PartiallyUnload();
                break;
            case RoomToLoad.ScreeningRoom:
                
                break;
            case RoomToLoad.VideoPrep:
                
                break;
            case RoomToLoad.Garden:
                
                break;
            case RoomToLoad.Conservatory:
                
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

    void TpToScreeningRoom()
    {
        if (turnDirection == TurnDirection.noTurn)
        {
            UIManager.instance.MainMenuUI.mainMenuPanel.SetActive(true);
            UIManager.instance.MainMenuUI.mainMenuPanel.gameObject.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1).OnComplete(
                () =>
                {
                    Environment.instance.SetActiveScreeningRoom(true);
                    Player.instance.SetCanMove(false);
                    Player.instance.animator.SetBool("isMoving",false);
                    Environment.instance.canTurnStage = false;
                        
                    Player.instance.TeleportPlayer(Environment.instance.playerTeleportPosScreeningRoom);
                    UnloadRoom();
                    StartCoroutine(DelayForTP());
                    UIManager.instance.MainMenuUI.FadeToTransparent(1);

                });
        }
        else
        {
            Environment.instance.SetActiveScreeningRoom(true);
        }
        
    }

    void TpToLivingRoom()
    {
        if (turnDirection == TurnDirection.noTurn)
        {
            UIManager.instance.MainMenuUI.mainMenuPanel.SetActive(true);
            UIManager.instance.MainMenuUI.mainMenuPanel.gameObject.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1).OnComplete(
                () =>
                {
                    Environment.instance.SetActiveLivingDiningRoom(true);
                    Player.instance.SetCanMove(false);
                    Player.instance.animator.SetBool("isMoving",false);
                    Environment.instance.canTurnStage = false;
                        
                    Player.instance.TeleportPlayer(Environment.instance.playerTeleportPosLivingRoom);
                    UnloadRoom();
                    StartCoroutine(DelayForTP());
                    UIManager.instance.MainMenuUI.FadeToTransparent(1);
                    //add delay here and reset properties and unload area
                });
        }
        else
        {
            Environment.instance.SetActiveLivingDiningRoom(true);
        }
    }
    
    void TpToEntrance()
    {
        if (turnDirection == TurnDirection.noTurn)
        {
            PartiallyUnloadRoom();
            UIManager.instance.MainMenuUI.mainMenuPanel.SetActive(true);
            UIManager.instance.MainMenuUI.mainMenuPanel.gameObject.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1).OnComplete(
                () =>
                {
                    Environment.instance.SetActiveEntrance(true);
                    Player.instance.SetCanMove(false);
                    Player.instance.animator.SetBool("isMoving",false);
                    Environment.instance.canTurnStage = false;
                    Environment.instance.turningEnviroment.transform.rotation = Quaternion.Euler(0,180,0); 
                    Player.instance.TeleportPlayer(Environment.instance.playerTeleportPosEntrance);
                    UnloadRoom();
                    StartCoroutine(DelayForTP());
                    UIManager.instance.MainMenuUI.FadeToTransparent(1);

                });
        }
        else
        {
            Environment.instance.SetActiveEntrance(true);
            
        }
    }
    void TpToBedroom()
    {
        if (turnDirection == TurnDirection.noTurn)
        {
            PartiallyUnloadRoom();
            UIManager.instance.MainMenuUI.mainMenuPanel.SetActive(true);
            UIManager.instance.MainMenuUI.mainMenuPanel.gameObject.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1).OnComplete(
                () =>
                {
                    Environment.instance.SetActiveBedroom(true);
                    Player.instance.SetCanMove(false);
                    Player.instance.animator.SetBool("isMoving",false);
                    Environment.instance.canTurnStage = false;
                    Environment.instance.turningEnviroment.transform.rotation = Quaternion.Euler(0,0,0);   
                    Player.instance.TeleportPlayer(Environment.instance.playerTeleportPosBedroom);
                    UnloadRoom();
                    StartCoroutine(DelayForTP());
                    UIManager.instance.MainMenuUI.FadeToTransparent(1);

                });
        }
        else
        {
            Environment.instance.SetActiveBedroom(true);
            
        }
    }

    IEnumerator DelayForTP()
    {
        yield return new WaitForSeconds(1);
        Player.instance.SetCanMove(true);
        Environment.instance.canTurnStage = true;
        gameObject.GetComponent<Collider>().enabled = true;

        switch (roomToLoad)
        {
            case RoomToLoad.Bedroom:
                Environment.instance.DeactivateEntranceToBedRoomDoor();
                break;
            case RoomToLoad.Entrance:
                Environment.instance.DeactivateBedroomToEntrance();
                break;
            case RoomToLoad.LivingDiningRoom:
                Environment.instance.DeactivateScreenToLivDoor();
                break;
            case RoomToLoad.ScreeningRoom:
                Environment.instance.DeactivateLivingToScrDoor();
                break;
            
        }
        
    }
    IEnumerator DelayForLoadingGardenAndConservatory()
    {
        // the timing is important, -0.5 so it loads the garden before the turning is complete, the scripts get unloaded at the end of turn.
        yield return new WaitForSeconds(Environment.instance.turnDuration - 0.5f);

        
        if (roomToLoad == RoomToLoad.VideoPrep && theRoomThisDoorBelong == RoomToLoad.ScreeningRoom)
        {
            Environment.instance.SetActiveGarden(true);
            Environment.instance.SetActiveConservatory(true);
        }
    }

    void VideoRoomToScreeningRoomExeption()
    {
        if (roomToLoad == RoomToLoad.ScreeningRoom && theRoomThisDoorBelong == RoomToLoad.VideoPrep)
        {
            Environment.instance.SetActiveGarden(false);
            Environment.instance.SetActiveConservatory(false);
        }
    }
    
    
}
