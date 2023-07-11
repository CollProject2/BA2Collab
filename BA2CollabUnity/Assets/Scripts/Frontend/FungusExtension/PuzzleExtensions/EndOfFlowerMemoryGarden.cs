using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfFlower","moves puzzle out of screen")]
public class EndOfFlowerMemoryGarden : Command
{
    public override void OnEnter()
    {
        Environment.instance.TeleportToEntrance();
        LightManager.instance.OpenDialogBoxLight(true);
        Continue();
    }
}
