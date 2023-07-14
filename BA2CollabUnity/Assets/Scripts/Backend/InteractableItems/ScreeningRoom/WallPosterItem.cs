using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPosterItem : InteractableItem
{
    public string posterGapMemory;

    protected override void Collect()
    {
        base.Collect();
        GetComponent<MeshRenderer>().enabled = true;
        Player.instance.SetCanMove(false);
        Player.instance.isSolving = true;
        UIManager.instance.dialogues.StartDialogue(posterGapMemory);
        SetIsComplete(true);
    }
}
