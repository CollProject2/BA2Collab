using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfFlower","moves puzzle out of screen")]
public class EndOfFlowerMemoryGarden : Command
{
    public Item hospitalPhotoPuzzle;
    public override void OnEnter()
    {
        Environment.instance.TeleportToEntrance();
        CameraManager.instance.CameraZoomOutFromGarden();
        hospitalPhotoPuzzle.enabled = true;
        Continue();
    }
}
