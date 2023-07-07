using DG.Tweening;
using UnityEngine;

public class FamilyPhotoFrameItem : InteractableItem
{
    [Header("object")]
    [SerializeField] protected GameObject framePuzzleObj;
    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    protected override void Awake()
    {
        base.Awake();
        hasToMove = true;
    }

    public override void InstantiateAndMove()
    {
        framePuzzleObj.SetActive(true);
        framePuzzleObj.transform.DOMove(activePos.position, itemMovementDuration);
        SetIsComplete(true);
    }

    public void MoveItemAway()
    {
        framePuzzleObj.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            framePuzzleObj.SetActive(false);
        });
    }

}
