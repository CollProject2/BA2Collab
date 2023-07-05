using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","move3DPuzzleAwayOffice_2","moves puzzle out of screen")]
public class Move3DPuzzleUIAway_Office_2 : Command
{
    public override void OnEnter()
    {
        BlockManager.instance.OnPuzzleFinishedMove();
        Continue();
    }
}
