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
        gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
        UIItemManager.instance.CollectItem(this);
        SetIsComplete(true);
    }
}
