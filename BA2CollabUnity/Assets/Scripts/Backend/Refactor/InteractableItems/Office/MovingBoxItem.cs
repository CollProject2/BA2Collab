using DG.Tweening;
using System;
using UnityEngine;

public class MovingBoxItem : InteractableItem
{
    public enum MovingBoxState
    {
        BearBox,
        PickedUp,
        Dropped,
        End
    }

    public MovingBoxState boxState;
    public Animator boxAnimator;
    public GameObject bearModel;
    public Item puzzleItemOffice3d;
    public string bearDropMemory;
    public string movingBoxDropMemory;
    public string endingMemory;

    protected override void Awake()
    {
        base.Awake();
        Player.instance.hasBear = false;
        Player.instance.hasMovingBox = false;
    }

    protected override void Update()
    {
        base.Update();
        UpdateBoxState();
    }

    protected override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (boxState)
            {
                case MovingBoxState.BearBox:
                    Collect();
                    break;
                case MovingBoxState.PickedUp:
                    MoveItemAway();
                    break;
                case MovingBoxState.Dropped:
                    Player.instance.RecallMemory(movingBoxDropMemory);
                    break;
                case MovingBoxState.End:
                    Player.instance.RecallMemory(endingMemory);
                    break;
            }
        }
    }

    public override void Collect()
    {
        // Put bear in the box
        boxAnimator.SetTrigger("PutBearInBox");
        ChangeValues();
        isInteractable = false;
        Player.instance.animator.SetBool("isMoving", false);
        ItemUIManager.Instance.ToggleItem(2);
        var bear = Instantiate(bearModel, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
        bear.transform.DOMove(transform.position, 1).OnComplete(() =>
        {
            bear.transform.parent = transform;
            puzzleItemOffice3d.SetIsHidden(false);
            isInteractable = true;
        });
    }

    public override void MoveItemAway()
    {
        // Move the box away
        isInteractable = false;
        transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            isInteractable = true;
            boxState = MovingBoxState.End;
        });
    }

    private void UpdateBoxState()
    {
        switch (boxState)
        {
            case MovingBoxState.BearBox:
                // Check the conditions for transitioning to the next state
                if (Player.instance.hasBear)
                {
                    boxState = MovingBoxState.PickedUp;
                }
                break;
            case MovingBoxState.PickedUp:
                // Check the conditions for transitioning to the next state
                if (!Player.instance.hasMovingBox)
                {
                    boxState = MovingBoxState.Dropped;
                }
                break;
        }
    }

    private void ChangeValues()
    {
        Player.instance.hasBear = false;
        Player.instance.hasMovingBox = true;
        Player.instance.RecallMemory(bearDropMemory);
    }

}
