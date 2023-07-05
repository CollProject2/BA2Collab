using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("puzzle","moveTextPuzzleAway","moves puzzle out of screen")]
public class MoveTextPuzzleAway : Command
{
    public TextFrameItem TextFrameItem;
    public MedallionItem medallionItem;
    public override void OnEnter()
    {
        TextFrameItem.MoveTextPuzzleAway();
        medallionItem.SetInteractable(true);
        Continue();
    }
}
