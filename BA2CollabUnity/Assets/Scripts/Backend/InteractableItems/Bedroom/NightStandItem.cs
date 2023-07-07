using DG.Tweening;
using System;
using UnityEngine;

public class NightStandItem : InteractableItem
{

    public GameObject cubeObject;
    [SerializeField] private GameObject cabinetDoor;
    [SerializeField] private Transform cubeActivePos;
    [SerializeField] private Transform cabinetDoorActivePos;
    [SerializeField] private float cabinetDoorMovementDuration;
    protected override void Awake()
    {
        base.Awake();
        hasToMove = true;
    }

    public override void InstantiateAndMove()
    {
        cabinetDoor.SetActive(true);
        cabinetDoor.transform.DOMove(cabinetDoorActivePos.position, cabinetDoorMovementDuration).OnComplete(() =>
        {
            cubeObject.transform.DOMove(cubeActivePos.position, itemMovementDuration).OnComplete(() =>
            {
                cubeObject.GetComponent<BlockCollectItem>().SetInteractable(true);
                SetIsComplete(true);
            });
        });
    }

}
