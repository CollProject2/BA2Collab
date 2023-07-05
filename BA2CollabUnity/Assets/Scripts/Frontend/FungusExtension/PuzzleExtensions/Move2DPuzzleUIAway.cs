using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("puzzle","move2DPuzzleAway","moves puzzle out of screen")]

public class Move2DPuzzleUIAway : Command
{
    public FamilyPhotoFrameItem frameItem;
    public override void OnEnter()
    {
        frameItem.MoveItemAway();
        LightManager.instance.OpenOfficeMovingBoxHighLight(true);
        Continue();
    }
}
