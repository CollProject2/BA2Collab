using UnityEngine;

public class PosterItem : InteractableItem
{
    public string posterMemory;


    public override void Collect()
    {
        interactParticle.SetActive(false);
        Player.instance.RecallMemory(posterMemory);
        Player.instance.hasPoster = true;
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        ItemUIManager.Instance.ToggleItem(0);
        SetIsComplete(true);
        Destroy(gameObject);
    }
}