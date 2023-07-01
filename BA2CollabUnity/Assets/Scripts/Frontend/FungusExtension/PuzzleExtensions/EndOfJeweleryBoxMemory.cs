using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfJeweleryBoxMemory","moves puzzle out of screen")]
public class EndOfJeweleryBoxMemory : Command
{
    public JeweleryBoxAndRing JeweleryBoxAndRing;
    public override void OnEnter()
    {
        JeweleryBoxAndRing.MoveMedallionAway();
    }
}
