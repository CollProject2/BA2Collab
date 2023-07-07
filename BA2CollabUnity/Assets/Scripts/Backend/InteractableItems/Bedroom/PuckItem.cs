using DG.Tweening;
using UnityEngine;

public class PuckItem : InteractableItem
{
    public string puckMemory;
    protected override void Collect()
    {
        base.Collect();

        UIManager.instance.dialogues.StartDialogue(puckMemory);

        Player.instance.hasBear = true;

        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        gameObject.GetComponentInChildren<Collider>().enabled = false;

        SetIsComplete(true);
    }
}
