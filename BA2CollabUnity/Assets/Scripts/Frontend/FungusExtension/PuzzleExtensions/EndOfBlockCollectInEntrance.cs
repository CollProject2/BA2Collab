using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

[CommandInfo("puzzle","EndOfBlockCollectInEntrance","moves puzzle out of screen")]
public class EndOfBlockCollectInEntrance : Command
{
    public override void OnEnter()
    {
        LightManager.instance.OpenEntranceLivingRoomDoorHighLight(true);
        Continue();
    }
}
