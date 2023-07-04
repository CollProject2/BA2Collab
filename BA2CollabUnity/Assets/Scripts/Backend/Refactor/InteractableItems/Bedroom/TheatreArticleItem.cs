using DG.Tweening;

public class TheatreArticleItem : InteractableItem
{
    public override void Collect()
    {
        //closes HUD when activating the puzzle 
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
            LightManager.instance.OpenBedroomOfficeDoorHighlights(true);
            Destroy(this);
        });
    }
}
