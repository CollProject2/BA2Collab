using DG.Tweening;
using UnityEngine;

public class TheatreArticleItem : InteractableItem
{
    [Header("object")]
    [SerializeField] protected GameObject itemObject;
    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    public override void Collect()
    {
        //closes HUD when activating the puzzle 
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        SetIsComplete(true);
    }

    public void InstantiateAndMove()
    {
        itemObject.SetActive(true);
        itemObject.transform.DOMove(activePos.position, itemMovementDuration);
    }

    public void MoveItemAway()
    {
        itemObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            itemObject.SetActive(false);
            LightManager.instance.OpenBedroomOfficeDoorHighlights(true);
            Destroy(this);
        });
    }
}
