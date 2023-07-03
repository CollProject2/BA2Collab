using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","Move3DPuzzleUIAwayConservatory_newfamilyPhoto","moves puzzle out of screen")]
public class Move3DPuzzleUIAwayConservatory_newfamilyPhoto : Command
{
    public PlantManager plantManager;
    public override void OnEnter()
    {
        BlockManager.instance.OnPuzzleFinishedMove();
        plantManager.isActive = true;
        Continue();
    }
}
