using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfJeweleryBoxMemory","moves puzzle out of screen")]
public class EndOfJeweleryBoxMemory : Command
{
    public JeweleryBoxAndRing JeweleryBoxAndRing;
    public Item protestPhotoItem;
    public override void OnEnter()
    {
        JeweleryBoxAndRing.MoveMedallionAway();
        protestPhotoItem.enabled = true;
        Continue();
    }
}
