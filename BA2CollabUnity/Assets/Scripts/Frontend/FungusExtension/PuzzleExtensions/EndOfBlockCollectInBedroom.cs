using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfBlockCollectInBedroom","moves puzzle out of screen")]

public class EndOfBlockCollectInBedroom : Command
{
    public override void OnEnter()
    {
        Continue();
    }
}
