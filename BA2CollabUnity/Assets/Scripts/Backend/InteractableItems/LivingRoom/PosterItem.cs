using UnityEngine;

public class PosterItem : InteractableItem
{
    public string posterMemory;

    protected override void Collect()
    {
        base.Collect();
        UIManager.instance.dialogues.StartDialogue(posterMemory);
        Player.instance.hasPoster = true;
        SetIsComplete(true);
    }
}