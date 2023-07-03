using DG.Tweening;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

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
        Garden,
        Conservatory,
    }
    //Singelton instance
    public static Environment instance = null;

    public StageMovingParts stageMovingParts;
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

        //make lists new init here so they are empty on awake
    }

    private void Start()
    {
        
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
        Player.instance.transform.parent = turningEnviroment.transform;
        Player.instance.SetCharacterController(false);
        turningEnviroment.transform.DORotate(turningEnviroment.transform.rotation.eulerAngles + new Vector3(0, -180, 0),2).SetEase(turnEase).OnComplete(
            ()=>
            {
                Player.instance.transform.parent = null;
                Player.instance.SetCharacterController(true);
                LightManager.instance.TurnOnPlayerLights();
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
            }
            else
            {
                trashedVideoRoom.SetActive(true);
            }
            
            videoDoor_scr.SetActive(true);
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
        }
        else
        {
            conservatory.SetActive(false);
        }
    }

    #endregion
    
}


