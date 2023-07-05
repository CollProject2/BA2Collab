using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","move3DPuzzleAwayLivingRoom","moves puzzle out of screen")]
public class Move3DPuzzleUIAwayLivingRoom : Command
{
    public MoonShineItem moonShineItem;
    public override void OnEnter()
    {
        BlockManager.instance.OnPuzzleFinishedMove();
        moonShineItem.SetInteractable(true);
        Continue();
    }
}
