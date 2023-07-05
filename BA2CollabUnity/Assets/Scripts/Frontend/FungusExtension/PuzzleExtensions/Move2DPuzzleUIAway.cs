using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("puzzle","move2DPuzzleAway","moves puzzle out of screen")]

public class Move2DPuzzleUIAway : Command
{
    public FrameItem frameItem;
    public BearZone bearZone;
    public override void OnEnter()
    {
        frameItem.Move2DPuzzleAway();
        bearZone.SetBoxActive();
        LightManager.instance.OpenOfficeMovingBoxHighLight(true);
        Continue();
    }
}
