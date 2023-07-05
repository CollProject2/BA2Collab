using DG.Tweening;
using System;
using UnityEngine;

public class SecretDrawerItem : InteractableItem
{
    public GameObject CubeObject;

    [Header("object")]
    [SerializeField] private GameObject secretCabinetDoor;

    public  void InstantiateAndMove()
    {
        SetIsComplete(true);
        secretCabinetDoor.transform.DOLocalRotate(new Vector3(-50, -121.215f, 0), 1).OnComplete(() =>
        {
            CubeObject.GetComponent<BlockCollectItem>().SetInteractable(true);
        });
    }

    public override void Collect()
    {
        InstantiateAndMove();
    }
}