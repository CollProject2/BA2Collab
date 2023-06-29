using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("Lights","OpenBedroomOfficeDoorHighlights","OpenBedroomOfficeDoorHighlights")]

public class OpenBedRoomOfficeDoorHighlights : Command
{
    public override void OnEnter()
    {
        LightManager.instance.OpenBedroomOfficeDoorHighlights(true);
        Continue();
    }
}
