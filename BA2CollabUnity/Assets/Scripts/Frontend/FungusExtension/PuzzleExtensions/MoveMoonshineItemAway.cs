using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("puzzle","movemoonshineLenternAwayLivingRoom","moves puzzle out of screen")]

public class MoveMoonshineItemAway : Command
{
    public MoonShineItem moonshineItem;
    public override void OnEnter()
    {
        moonshineItem.MoveMoonshineLenternAway();
        Continue();
    }
}
