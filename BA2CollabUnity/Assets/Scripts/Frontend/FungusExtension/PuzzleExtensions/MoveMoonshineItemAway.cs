using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("puzzle","movemoonshineLenternAwayLivingRoom","moves puzzle out of screen")]

public class MoveMoonshineItemAway : Command
{
    public MoonShineItem moonshineItem;
    public GameObject triggerZoneToEntrance;
    public override void OnEnter()
    {
        moonshineItem.MoveMoonshineLenternAway();
        triggerZoneToEntrance.SetActive(true);
        
        Continue();
    }
}
