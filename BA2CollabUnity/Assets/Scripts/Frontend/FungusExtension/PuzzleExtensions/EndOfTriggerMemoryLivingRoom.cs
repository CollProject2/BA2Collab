using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

[CommandInfo("puzzle","endOfTriggerMemoryLivingRoom","moves puzzle out of screen")]

public class EndOfTriggerMemoryLivingRoom : Command
{
    public override void OnEnter()
    {
        LightManager.instance.OpenEntranceBedRoomDoorHighLight(true);
        Continue();
    }
}
