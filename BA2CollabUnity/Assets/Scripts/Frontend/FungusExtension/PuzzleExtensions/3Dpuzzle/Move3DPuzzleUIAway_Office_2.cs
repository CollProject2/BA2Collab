using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","move3DPuzzleAwayOffice_2","moves puzzle out of screen")]
public class Move3DPuzzleUIAway_Office_2 : Command
{
    public JeweleryBoxAndRing jeweleryBoxAndRing;
    public Item officeItem3dpuzzle;
    public override void OnEnter()
    {
        BlockManager.instance.OnPuzzleFinishedMove();
        jeweleryBoxAndRing.isInteractable = true;
        Destroy(officeItem3dpuzzle);
        Continue();
    }
}
