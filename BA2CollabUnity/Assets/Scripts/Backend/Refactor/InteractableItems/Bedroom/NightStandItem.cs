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

    protected override void Update()
    {
        base.Update();
        if (isInteractable && isActiveAndEnabled)
        {
            InstantiateAndMove();
            isInteractable = false;
        }
    }

    public override void Collect()
    {
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        isInteractable = false;
    }

    public override void MoveItemAway()
    {
        cubeObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            cubeObject.SetActive(false);
            Destroy(this);
        });
    }

    protected override void InstantiateAndMove()
    {
        cabinetDoor.SetActive(true);
        cabinetDoor.transform.DOMove(cabinetDoorActivePos.position, cabinetDoorMovementDuration).OnComplete(() =>
        {
            cubeObject.transform.DOMove(cubeActivePos.position, itemMovementDuration).OnComplete(() =>
            {
                cubeObject.GetComponent<BlockCollect>().SetInteractable(true);
                Destroy(this);
            });
        });
    }

    internal bool HasCollectedBlock()
    {
        throw new NotImplementedException();
    }

    internal void Collect3DPuzzleBlock()
    {
        throw new NotImplementedException();
    }
}
