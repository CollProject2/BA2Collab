using DG.Tweening;
using System;
using UnityEngine;

public class SecretDrawerItem : InteractableItem
{
    public GameObject CubeObject;

    [Header("object")]
    [SerializeField] private GameObject secretCabinetDoor;
    protected override void Awake()
    {
        base.Awake();
        hasToMove = true;
    }

    public override void InstantiateAndMove()
    {
        secretCabinetDoor.transform.DOLocalRotate(new Vector3(-50, -121.215f, 0), 1).OnComplete(() =>
        {
            CubeObject.GetComponent<BlockCollectItem>().SetInteractable(true);
            SetIsComplete(true);
        });
    }

}