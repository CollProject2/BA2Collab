using DG.Tweening;

public class PuckItem : InteractableItem
{
    public string puckMemory;
    public override void Collect()
    {
        interactParticle.SetActive(false);
        UIManager.instance.dialogues.StartDialogue(puckMemory);


        Player.instance.hasBear = true;
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);

        ItemUIManager.Instance.ToggleItem(2);
        SetIsComplete(true);
        Destroy(gameObject);
    }

}
