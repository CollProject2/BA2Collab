using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockCollectItem : InteractableItem
{
    public string BlockCollectMemory;

    protected override void Collect()
    {
        base.Collect();
        Player.instance.missingBlocks--;
        UIManager.instance.dialogues.StartDialogue(BlockCollectMemory);
        UIItemManager.instance.CollectItem(this);
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        SetIsComplete(true);
    }
}
