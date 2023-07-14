using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfFlower","moves puzzle out of screen")]
public class EndOfFlowerMemoryGarden : Command
{
    public GameObject endGameColliders;
    public override void OnEnter()
    {
        Environment.instance.TeleportToEntrance();
        endGameColliders.SetActive(true);
        LightManager.instance.OpenMiddleLight(true);
        LightManager.instance.OpenDialogBoxLight(true);
        Continue();
    }
}
