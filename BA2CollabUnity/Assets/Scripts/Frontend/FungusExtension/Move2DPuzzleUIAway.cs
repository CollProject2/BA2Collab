using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("puzzle","move2DPuzzleAway","moves puzzle out of screen")]

public class Move2DPuzzleUIAway : Command
{
    public FrameItem frameItem;
    public override void OnEnter()
    {
        frameItem.Move2DPuzzleAway();
        Continue();
    }
}
