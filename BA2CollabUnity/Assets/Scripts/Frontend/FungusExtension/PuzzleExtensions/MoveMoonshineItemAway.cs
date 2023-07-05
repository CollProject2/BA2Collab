using Fungus;
using UnityEngine;
[CommandInfo("puzzle", "movemoonshineLenternAwayLivingRoom", "moves puzzle out of screen")]

public class MoveMoonshineItemAway : Command
{
    public MoonShineLanternItem moonshineItem;
    public GameObject triggerZoneToEntrance;
    public override void OnEnter()
    {
        moonshineItem.MoveMoonshineLanternAway();
        triggerZoneToEntrance.SetActive(true);
        Continue();
    }
}
