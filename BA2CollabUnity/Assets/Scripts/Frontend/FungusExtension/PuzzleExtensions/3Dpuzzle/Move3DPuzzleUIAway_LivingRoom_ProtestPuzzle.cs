using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","move3DPuzzleAwayLivingRoom_ProtestPuzzle","moves puzzle out of screen")]

public class Move3DPuzzleUIAway_LivingRoom_ProtestPuzzle : Command
{
    public Poster poster;
    public Item livingRoom_ProtestPuzzleItem;
    public override void OnEnter()
    {
        BlockManager.instance.OnPuzzleFinishedMove();
        poster.SetInteractable(true);
        Destroy(livingRoom_ProtestPuzzleItem);
        Continue();
        
    }
}
