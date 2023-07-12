using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","StartCutScene","moves puzzle out of screen")]
public class StartCutScene : Command
{
    public override void OnEnter()
    {
        StoryManager.instance.movingBoxItem.StartCutscene();
        Continue();
    }
}
