using DG.Tweening;
using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{

    //Singelton instance
    public static Environment instance = null;
    //Properties
    [Header("Stage Turn")]
    public bool canTurnStage = true;
    [SerializeField] private GameObject turningEnviroment;
    [SerializeField][CanBeNull] private GameObject stageGround;
    [SerializeField] private GameObject groundChecker;
    [SerializeField] private float turnDuration;
    [SerializeField] private AnimationCurve turnEase;
    public List<DoorAdjuster> doors;

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
        turningEnviroment.transform.DORotate(turningEnviroment.transform.rotation.eulerAngles + new Vector3(0, 60, 0), turnDuration).SetEase(turnEase).OnComplete(EndOfTurn);
    }

    public void TurnEnvironmentCounterClockWise()
    {
        AdjustAllDoorsOnTurn();
        Player.instance.SetCanMove(false);
        Player.instance.transform.parent = stageGround.transform;
        canTurnStage = false;
        turningEnviroment.transform.DORotate(turningEnviroment.transform.rotation.eulerAngles + new Vector3(0, -60, 0), turnDuration).SetEase(turnEase).OnComplete(EndOfTurn);
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
        if (doors == null) return;
        foreach (var door in doors)
        {
            door.AdjustDoors();
        }
    }

}
