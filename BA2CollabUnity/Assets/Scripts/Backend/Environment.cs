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
    //Properties
    [Header("Rooms")]
    [SerializeField] private GameObject bedroom;
    [SerializeField] private GameObject office;
    [SerializeField] private GameObject entrance;
    [SerializeField] private GameObject livingRoom;
    [SerializeField] private GameObject screeningRoom;
    [SerializeField] private GameObject videoRoom;
    [SerializeField] private GameObject garden;
    [SerializeField] private GameObject conservatory;
    [Header("Stage Turn")]
    public bool canTurnStage = true;
    public GameObject turningEnviroment;
    [CanBeNull] public GameObject stageGround;
    [SerializeField] private GameObject groundChecker;
    public float turnDuration;
    public AnimationCurve turnEase;
    
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
        //AdjustAllDoorsOnTurn();
        currentRoom = CurrentRoom.Bedroom;
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

    
    //adjusts all doors on start and on each turn call.
    // void AdjustAllDoorsOnTurn()
    // {
    //     if (doors == null) return;
    //     foreach (var door in doors)
    //     {
    //         //door.AdjustDoors();
    //     }
    // }
    
    // Rooms

    public void SetActiveBedroom(bool roomState)
    {
        if (roomState)
        {
            bedroom.SetActive(true);
        }
        else
        {
            bedroom.SetActive(false);
        }
    }
    public void SetActiveOffice(bool roomState)
    {
        if (roomState)
        {
            office.SetActive(true);
        }
        else
        {
            office.SetActive(false);
        }
    }
    public void SetActiveEntrance(bool roomState)
    {
        if (roomState)
        {
            entrance.SetActive(true);
        }
        else
        {
            entrance.SetActive(false);
        }
    }
    public void SetActiveLivingDiningRoom(bool roomState)
    {
        if (roomState)
        {
            livingRoom.SetActive(true);
        }
        else
        {
            livingRoom.SetActive(false);
        }
    }
    public void SetActiveScreeningRoom(bool roomState)
    {
        if (roomState)
        {
            screeningRoom.SetActive(true);
        }
        else
        {
            screeningRoom.SetActive(false);
        }
    }
    public void SetActiveVideoPrep(bool roomState)
    {
        if (roomState)
        {
            videoRoom.SetActive(true);
        }
        else
        {
            videoRoom.SetActive(false);
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
}


