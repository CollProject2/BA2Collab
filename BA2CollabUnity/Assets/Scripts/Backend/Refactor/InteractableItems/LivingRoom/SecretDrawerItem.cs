using DG.Tweening;
using System;
using UnityEngine;

public class SecretDrawerItem : InteractableItem
{
    public GameObject CubeObject;

    [Header("object")]
    [SerializeField] private GameObject secretCabinetDoor;

    protected override void Update()
    {
        base.Update();
        if (isInteractable)
        {
            InstantiateAndMove();
            isInteractable = false;
        }
    }

    public override void Collect()
    {
        // UI manager stuff
    }

    public override void MoveItemAway()
    {
        secretCabinetDoor.transform.DOLocalRotate(new Vector3(-50, -121.215f, 0), 1).OnComplete(() =>
        {
            CubeObject.GetComponent<BlockCollect>().SetInteractable(true);
            Destroy(this);
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