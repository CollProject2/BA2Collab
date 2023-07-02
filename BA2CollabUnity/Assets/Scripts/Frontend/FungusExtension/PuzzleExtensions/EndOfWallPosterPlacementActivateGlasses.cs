using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfWallPosterActivateGlasses","moves puzzle out of screen")]
public class EndOfWallPosterPlacementActivateGlasses :Command
{
    public GlassesItem glassesItem;
    public override void OnEnter()
    {
        glassesItem.isInteractable = true;
    }
}
