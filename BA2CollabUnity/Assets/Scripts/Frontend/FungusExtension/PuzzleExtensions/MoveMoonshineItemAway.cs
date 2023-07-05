using Fungus;
using UnityEngine;
[CommandInfo("puzzle", "movemoonshineLenternAwayLivingRoom", "moves puzzle out of screen")]

public class MoveMoonshineItemAway : Command
{
    public MoonShineItem moonshineItem;
    public GameObject triggerZoneToEntrance;
    public LissandrasCabinet lissandrasCabinet;
    public override void OnEnter()
    {
        moonshineItem.MoveMoonshineLenternAway();
        triggerZoneToEntrance.SetActive(true);
        lissandrasCabinet.isInteractable = true;
        Continue();
    }
}
