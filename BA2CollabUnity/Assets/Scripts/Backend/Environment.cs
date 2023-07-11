using DG.Tweening;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Environment : MonoBehaviour
{
    public enum CurrentRoom
    {
        Office,
        Bedroom,
        Entrance,
        LivingDiningRoom,
        ScreeningRoom,
        VideoPrep,
        TrashedVideoPrep,
        Garden,
        Conservatory,
    }
    //Singelton instance
    public static Environment instance = null;


    //Properties
    [Header("Rooms")]
    [SerializeField] private GameObject bedroom;
    [SerializeField] private GameObject office;
    [SerializeField] private GameObject entrance;
    [SerializeField] private GameObject livingRoom;
    [SerializeField] private GameObject screeningRoom;
    [SerializeField] private GameObject videoRoom;
    [SerializeField] private GameObject trashedVideoRoom;
    [SerializeField] private GameObject garden;
    [SerializeField] private GameObject conservatory;

    [Header("Partially Loaded Part")]
    [CanBeNull] public PartialRoomLoader partialLoader_BedRoom;
    [CanBeNull] public PartialRoomLoader partialLoader_Office;
    [CanBeNull] public PartialRoomLoader partialLoader_LivingRoom;

    [Header("Doors")]
    [SerializeField] private GameObject bedroomDoor_ent;
    [SerializeField] private GameObject bedroomDoor_ofc;
    [SerializeField] private GameObject officeDoor_bed;
    [SerializeField] private GameObject entrenceDoor_bed;
    [SerializeField] private GameObject entranceDoor_liv;
    [SerializeField] private GameObject livingDoor_ent;
    [SerializeField] private GameObject livingDoor_scr;
    [SerializeField] private GameObject screenDoor_liv;
    [SerializeField] private GameObject screenDoor_vid;
    [SerializeField] private GameObject videoDoor_scr;
    [Header("Stage Turn")]
    public bool canTurnStage = true;
    public GameObject turningEnviroment;
    [CanBeNull] public GameObject stageGround;
    [SerializeField] private GameObject groundChecker;
    public float turnDuration;
    public AnimationCurve turnEase;
    public Transform playerTeleportPosScreeningRoom;
    public Transform playerTeleportPosLivingRoom;
    public Transform playerTeleportPosEntrance;
    public Transform playerTeleportPosBedroom;
    [Header("EntranceExeptionObjects")]
    [SerializeField] private GameObject entranceNote;
    [SerializeField] private GameObject entranceImage;
    public bool trashTheVideoRoom;

    public CurrentRoom currentRoom;
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

        if (entranceNote != null)
            entranceNote.SetActive(false);
        entranceImage.SetActive(false);
    }


    private void Update()
    {
        GroundObjectCheck();
    }

    void GroundObjectCheck()
    {
        var ray = new Ray(groundChecker.transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.collider.tag == "StageGround")
            {
                stageGround = hit.transform.gameObject;
            }
        }
    }

    public void TurnEnvironmentAtStart()
    {
        partialLoader_BedRoom.PartialLoad();
        LightManager.instance.ChangeColorBG(LightManager.instance.bedRoomColor);
        LightManager.instance.ChangeColorOverHead(LightManager.instance.overHeadBedRoomColor);
        Player.instance.transform.parent = turningEnviroment.transform;
        Player.instance.SetCharacterController(false);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.roomChange, new Vector3(0,0,0));
        turningEnviroment.transform.DORotate(turningEnviroment.transform.rotation.eulerAngles + new Vector3(0, -180, 0), 2).SetEase(turnEase).OnComplete(
            () =>
            {
                Player.instance.transform.parent = null;
                Player.instance.SetCharacterController(true);
                currentRoom = CurrentRoom.Bedroom;
                LightManager.instance.TurnOnPlayerLights();
                LightManager.instance.OpenOverHeadLights(true);
                LightManager.instance.OpenMiddleLight(true);
                LightManager.instance.ChangeColorOverHead(LightManager.instance.overHeadBedRoomColor);
                DeactivateEntranceToBedRoomDoor();
                SetActiveEntrance(false);
            });
    }


    //Doors 
    // these doors have slightly different functionalities, Unloading them separetely
    public void DeactivateLivingToScrDoor()
    {
        livingDoor_scr.SetActive(false);
    }

    public void DeactivateScreenToLivDoor()
    {
        screenDoor_liv.SetActive(false);
    }

    public void DeactivateEntranceToBedRoomDoor()
    {
        entrenceDoor_bed.SetActive(false);
    }

    public void DeactivateBedroomToEntrance()
    {
        bedroomDoor_ent.SetActive(false);
    }
    // Rooms

    #region RoomLoaderUnloaders

    public void SetActiveBedroom(bool roomState)
    {
        if (roomState)
        {
            bedroom.SetActive(true);
            bedroomDoor_ent.SetActive(true);
            bedroomDoor_ofc.SetActive(true);
            currentRoom = CurrentRoom.Bedroom;
            partialLoader_BedRoom.PartialLoad();
            LightManager.instance.ChangeColorBG(LightManager.instance.bedRoomColor);
            LightManager.instance.ChangeColorOverHead(LightManager.instance.overHeadBedRoomColor);
        }
        else
        {
            bedroom.SetActive(false);
            bedroomDoor_ofc.SetActive(false);
            LightManager.instance.OpenBedroomOfficeDoorHighlights(false);
        }
    }
    public void SetActiveOffice(bool roomState)
    {
        if (roomState)
        {
            office.SetActive(true);
            officeDoor_bed.SetActive(true);
            currentRoom = CurrentRoom.Office;
            partialLoader_Office.PartialLoad();
            LightManager.instance.ChangeColorBG(LightManager.instance.officeColor);
            LightManager.instance.ChangeColorOverHead(LightManager.instance.overHeadOfficeColor);
        }
        else
        {
            office.SetActive(false);
            officeDoor_bed.SetActive(false);
        }
    }
    public void SetActiveEntrance(bool roomState)
    {
        if (roomState)
        {
            entrance.SetActive(true);
            entranceDoor_liv.SetActive(true);
            entrenceDoor_bed.SetActive(true);
            currentRoom = CurrentRoom.Entrance;
            LightManager.instance.ChangeColorBG(LightManager.instance.entranceRoomColor); // experimental
            LightManager.instance.ChangeColorOverHead(LightManager.instance.overHeadEntranceRoomColor);
            LightManager.instance.OpenMiddleLight(true);
            LightManager.instance.OpenEntranceDoorLights(true);
            if (entranceNote != null)
                entranceNote.SetActive(true);
            entranceImage.SetActive(true);
        }
        else
        {
            entrance.SetActive(false);
            entranceDoor_liv.SetActive(false);
        }
    }
    public void SetActiveLivingDiningRoom(bool roomState)
    {
        if (roomState)
        {
            livingRoom.SetActive(true);
            livingDoor_ent.SetActive(true);
            livingDoor_scr.SetActive(true);
            currentRoom = CurrentRoom.LivingDiningRoom;
            LightManager.instance.ChangeColorBG(LightManager.instance.livingRoomColor);
            LightManager.instance.ChangeColorOverHead(LightManager.instance.overHeadLivingRoomColor);
            partialLoader_LivingRoom.PartialLoad();
        }
        else
        {
            livingRoom.SetActive(false);
            livingDoor_ent.SetActive(false);
            LightManager.instance.OpenLivingRoomEntranceDoorHighLight(false);
        }
    }
    public void SetActiveScreeningRoom(bool roomState)
    {
        if (roomState)
        {
            screeningRoom.SetActive(true);
            screenDoor_liv.SetActive(true);
            screenDoor_vid.SetActive(true);
            currentRoom = CurrentRoom.ScreeningRoom;
            LightManager.instance.ChangeColorBG(LightManager.instance.screeningRoomColor);
            LightManager.instance.ChangeColorOverHead(LightManager.instance.overHeadScreeningRoomColor);
            LightManager.instance.OpenMiddleLight(true);
        }
        else
        {
            screeningRoom.SetActive(false);
            screenDoor_vid.SetActive(false);
        }
    }
    public void SetActiveVideoPrep(bool roomState)
    {
        if (roomState)
        {
            if (!trashTheVideoRoom)
            {
                videoRoom.SetActive(true);
                LightManager.instance.OpenMiddleLight(false);
                currentRoom = CurrentRoom.VideoPrep;
            }
            else
            {
                trashedVideoRoom.SetActive(true);
                LightManager.instance.OpenMiddleLight(false);
                currentRoom = CurrentRoom.TrashedVideoPrep;
            }

            videoDoor_scr.SetActive(true);
            LightManager.instance.ChangeColorBG(LightManager.instance.VideoColor);
            LightManager.instance.ChangeColorOverHead(LightManager.instance.overHeadVideoColor);
        }
        else
        {
            videoRoom.SetActive(false);
            trashedVideoRoom.SetActive(false);
            videoDoor_scr.SetActive(false);
        }
    }
    public void SetActiveGarden(bool roomState)
    {
        if (roomState)
        {
            garden.SetActive(true);
            currentRoom = CurrentRoom.Garden;
        }
        else
        {
            garden.SetActive(false);
        }
    }
    public void SetActiveConservatory(bool roomState)
    {
        if (roomState)
        {
            conservatory.SetActive(true);
            currentRoom = CurrentRoom.Conservatory;
        }
        else
        {
            conservatory.SetActive(false);
        }
    }

    #endregion

    public void TeleportToEntrance()
    {
        UIManager.instance.MainMenuUI.mainMenuPanel.SetActive(true);
        UIManager.instance.MainMenuUI.mainMenuPanel.gameObject.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1).OnComplete(
            () =>
            {
                SetActiveEntrance(true);
                Player.instance.SetCanMove(false);
                Player.instance.animator.SetBool("isMoving", false);
                canTurnStage = false;
                Player.instance.inGarden = false;
                Player.instance.TeleportPlayer(playerTeleportPosEntrance);
                SetActiveConservatory(false);
                SetActiveGarden(false);
                SetActiveVideoPrep(false);
                StartCoroutine(DelayForEntranceTP(1));
                UIManager.instance.MainMenuUI.FadeToTransparent(1);
                CameraManager.instance.CameraZoomOutFromGarden();

            });
    }

    IEnumerator DelayForEntranceTP(float duration)
    {
        yield return new WaitForSeconds(duration);
        Player.instance.SetCanMove(true);
        canTurnStage = true;


    }

}


