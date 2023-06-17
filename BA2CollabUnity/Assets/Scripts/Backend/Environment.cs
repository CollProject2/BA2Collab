using System;
using System.Collections.Generic;
using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;

public class Environment : MonoBehaviour
{
    
    //Singelton instance
    public static Environment instance = null;
    //Properties
    public List<Item> items { get; private set; }
    public List<Puzzle> puzzles { get; private set; }
    public List<PlayerMemory> memory { get; private set; }
    public float rotationAngle { get; private set; }
    public List<Vector3> rotationPoints { get; private set; }

    [Header("Stage Turn")]
    public bool canTurnStage = true;
    [SerializeField] private GameObject turningEnviroment;
    [SerializeField] [CanBeNull] private GameObject stageGround;
    [SerializeField] private GameObject groundChecker;
    [SerializeField] private float turnDuration;
    [SerializeField] private AnimationCurve turnEase;
    public List<DoorAdjuster> doors;



    //Init
    public void InitializeEnvironment(List<Item> itemList, List<Puzzle> puzzleList, List<PlayerMemory> memoryList)
    {
        items = itemList;
        puzzles = puzzleList;
        memory = memoryList;
        rotationAngle = 0.0f;
        rotationPoints = new List<Vector3>();
    }

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
        AdjustAllDoorsOnTurn();
    }

    private void Update()
    {
        GroundObjectCheck();
    }

    //Methods
    public void Display()
    {
       gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Rotate(float newAngle)
    {
        rotationAngle = newAngle;
    }

    public void CheckForRotationPoint(Vector3 playerPosition)
    {
        foreach (var e in rotationPoints)
        {
            if (playerPosition == e)
                Rotate(5);//test value
        }
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

    public void TurnEnvironmentClockWise()
    {
        AdjustAllDoorsOnTurn();
        Player.instance.SetCanMove(false);
        Player.instance.transform.parent = stageGround.transform;
        canTurnStage = false;
        turningEnviroment.transform.DORotate(turningEnviroment.transform.rotation.eulerAngles + new Vector3(0,60,0), turnDuration).SetEase(turnEase).OnComplete(EndOfTurn);
    }

    public void TurnEnvironmentCounterClockWise()
    {
        AdjustAllDoorsOnTurn();
        Player.instance.SetCanMove(false);
        Player.instance.transform.parent = stageGround.transform;
        canTurnStage = false;
        turningEnviroment.transform.DORotate(turningEnviroment.transform.rotation.eulerAngles + new Vector3(0,-60,0), turnDuration).SetEase(turnEase).OnComplete(EndOfTurn);
    }
    
    void EndOfTurn()
    {
        Player.instance.SetCanMove(true);
        Player.instance.transform.parent = null;
        canTurnStage = true;
    }
    //adjusts all doors on start and on each turn call.
    void AdjustAllDoorsOnTurn()
    {
        foreach (var door in doors)
        {
            door.AdjustDoors();
        }
    }
    
}
