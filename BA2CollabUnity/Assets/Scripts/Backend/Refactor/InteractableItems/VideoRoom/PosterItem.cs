using UnityEngine;

public class PosterItem : InteractableItem
{
    public string posterMemory;

    protected override void Interact()
    {
        base.Interact();
    }

    public override void Collect()
    {
        interactParticle.SetActive(false);
        Player.instance.RecallMemory(posterMemory);
        Player.instance.hasPoster = true;
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        isInteractable = false;
        ItemUIManager.Instance.ToggleItem(0);
        Destroy(gameObject);
    }

    public override void MoveItemAway()
    {
        // Not needed in this case
    }
}