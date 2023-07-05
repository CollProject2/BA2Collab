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


    public override void Collect()
    {
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        SetIsComplete(true);
        
    }

    protected void InstantiateAndMove()
    {
        cabinetDoor.SetActive(true);
        cabinetDoor.transform.DOMove(cabinetDoorActivePos.position, cabinetDoorMovementDuration).OnComplete(() =>
        {
            cubeObject.transform.DOMove(cubeActivePos.position, itemMovementDuration).OnComplete(() =>
            {
                cubeObject.GetComponent<BlockCollectItem>().SetInteractable(true);
                Destroy(this);
            });
        });
    }

}
