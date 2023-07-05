using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("puzzle","moveTextPuzzleAway","moves puzzle out of screen")]
public class MoveTextPuzzleAway : Command
{
    public DegreeItem degreeItem;
    public MedallionItem medallionItem;
    public override void OnEnter()
    {
        degreeItem.MoveItemAway();
        medallionItem.SetInteractable(true);
        Continue();
    }
}
