using DG.Tweening;

public class FamilyPhotoFrameItem : InteractableItem
{

    public override void Collect()
    {
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        isInteractable = false;
    }

    public override void MoveItemAway()
    {
        itemObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(() =>
        {
            itemObject.SetActive(false);
            Destroy(this);
        });
    }


}
