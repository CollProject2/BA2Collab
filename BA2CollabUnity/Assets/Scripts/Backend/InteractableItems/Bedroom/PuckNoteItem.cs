using DG.Tweening;
using UnityEngine;

public class PuckNoteItem : InteractableItem
{
    [Header("object")] [SerializeField] protected GameObject itemObject;
    [Header("positions")] [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    private bool canExitNote;


    protected override void Awake()
    {
        base.Awake();
        canExitNote = false;
    }

    protected override void Collect()
    {
        base.Collect();
        itemObject.transform.DOMove(activePos.position, itemMovementDuration).OnComplete(()=> canExitNote = true);
    }

    protected override void Interact()
    {
        if (!canExitNote)
        {
            base.Interact();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Q))
                MoveItemAway();
        }
    }

    public void MoveItemAway()
    {
        Player.instance.SetCanMove(true);
        SetIsComplete(true);
        itemObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            itemObject.SetActive(false);
        });
    }
}