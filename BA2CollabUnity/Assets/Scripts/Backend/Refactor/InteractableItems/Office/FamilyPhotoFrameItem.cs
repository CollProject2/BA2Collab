using DG.Tweening;
using UnityEngine;

public class FamilyPhotoFrameItem : InteractableItem
{
    [Header("object")]
    [SerializeField] protected GameObject framePuzzleObj;
    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;

    public override void Collect()
    {
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        SetIsComplete(true);

    }

    public void InstantiateAndMove()
    {
        framePuzzleObj.SetActive(true);
        framePuzzleObj.transform.DOMove(activePos.position, itemMovementDuration);
    }

    public void MoveItemAway()
    {
        framePuzzleObj.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            framePuzzleObj.SetActive(false);
            Destroy(this);
        });
    }

}
