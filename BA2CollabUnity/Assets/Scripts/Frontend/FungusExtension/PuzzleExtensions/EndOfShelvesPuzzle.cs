using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfShelvesPuzzle","moves puzzle out of screen")]

public class EndOfShelvesPuzzle : Command
{
    public FirePlaceSecretDrawer firePlaceSecretDrawer;

    public override void OnEnter()
    {
        firePlaceSecretDrawer.isInteractable = true;
        Continue();
    }
}
