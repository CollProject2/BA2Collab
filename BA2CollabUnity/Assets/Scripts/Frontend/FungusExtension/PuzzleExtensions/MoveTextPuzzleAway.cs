using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("puzzle","moveTextPuzzleAway","moves puzzle out of screen")]
public class MoveTextPuzzleAway : Command
{
    public FamilyPhotoFrameItem TextFrameItem;
    public MedallionItem medallionItem;
    public override void OnEnter()
    {
        TextFrameItem.MoveItemAway();
        medallionItem.SetInteractable(true);
        Continue();
    }
}
