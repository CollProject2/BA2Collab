using DG.Tweening;
using UnityEngine;

public class MoonShineLanternItem : InteractableItem
{
    [Header("object")]
    [SerializeField] protected GameObject itemObject;
    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    public override void Collect()
    {
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        SetIsComplete(true);
    }

    public void MoveItemAway()
    {
        itemObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            itemObject.SetActive(false);
            Destroy(this);
        });
    }


}
