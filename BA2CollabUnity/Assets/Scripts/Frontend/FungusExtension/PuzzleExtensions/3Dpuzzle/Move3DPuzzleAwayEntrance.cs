using Fungus;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CommandInfo("puzzle", "move3dPuzzleAwayEntrance", "moves puzzle out of screen")]
public class Move3DPuzzleAwayEntrance : Command
{
    public override void OnEnter()
    {
        BlockManager.instance.OnPuzzleFinishedMove();
        Continue();
    }
}
