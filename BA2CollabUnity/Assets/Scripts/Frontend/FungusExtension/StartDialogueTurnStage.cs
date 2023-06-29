using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("OnStart","TurnStageOnstart","Turn the stage after dialogue is done")]

public class StartDialogueTurnStage : Command
{
    public override void OnEnter()
    {
        Environment.instance.TurnEnvironmentAtStart();
        Continue();
    }
}
