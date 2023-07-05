using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockCollectItem : InteractableItem
{
    public string BlockCollectMemory;

    public override void Collect()
    {
        //closes HUD when activating the puzzle 
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        Player.instance.missingBlocks--;
        ItemUIManager.Instance.ToggleItem(6 - Player.instance.missingBlocks);
        SetIsComplete(true);
        UIManager.instance.dialogues.StartDialogue(BlockCollectMemory);
        Destroy(gameObject);
    }
}
