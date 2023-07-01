using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("puzzle","move3DPuzzleAway","moves puzzle out of screen")]

public class Move3DPuzzleUIAway : Command
{
    public MovingBox2 movingBox2;
    public override void OnEnter()
    {
        BlockManager.instance.OnPuzzleFinishedMove();
        movingBox2.SetBoxActive();
        Continue();
    }
}
