using DG.Tweening;
using UnityEngine;

public class TheatreArticleItem : InteractableItem
{
    [Header("object")]
    [SerializeField] protected GameObject itemObject;
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
        itemObject.SetActive(true);
        itemObject.transform.DOMove(activePos.position, itemMovementDuration);
        SetIsComplete(true);
    }

    public void MoveItemAway()
    {
        itemObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            itemObject.SetActive(false);
            LightManager.instance.OpenBedroomOfficeDoorHighlights(true);
        });
    }
}
