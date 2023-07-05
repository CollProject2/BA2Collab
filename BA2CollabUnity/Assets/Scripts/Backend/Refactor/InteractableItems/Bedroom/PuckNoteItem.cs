using DG.Tweening;
using UnityEngine;

public class PuckNoteItem : InteractableItem
{
    [Header("object")] [SerializeField] protected GameObject itemObject;
    [Header("positions")] [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    private bool canExitNote;
    private bool doOnce;

    protected override void Awake()
    {
        base.Awake();
        canExitNote = false;
        doOnce = true;
    }

    public override void Collect()
    {
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        itemObject.transform.DOMove(activePos.position, itemMovementDuration).OnComplete(()=> canExitNote = true);
        doOnce = false;
    }

    protected override void Interact()
    {
        if (!canExitNote && doOnce)
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
        isComplete = true;
        itemObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            itemObject.SetActive(false);
        });
    }
}