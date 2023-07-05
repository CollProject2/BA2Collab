using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPosterItem : InteractableItem
{
    public string posterGapMemory;
    protected override void Interact()
    {
        base.Interact();
    }

    public override void Collect()
    {
        GetComponent<MeshRenderer>().enabled = true;
        isInteractable = false;
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        Player.instance.RecallMemory(posterGapMemory);
        SetIsComplete(true);
    }
}
