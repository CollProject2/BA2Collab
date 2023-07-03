using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CommandInfo("puzzle", "move3dPuzzleAwayEntrance", "moves puzzle out of screen")]
public class Move3DPuzzleAwayEntrance : Command
{
    public MovingBox3 movingBox3;
    public override void OnEnter()
    {
        BlockManager.instance.OnPuzzleFinishedMove();
        movingBox3.isInteractable = true;
        Continue();
    }
}
