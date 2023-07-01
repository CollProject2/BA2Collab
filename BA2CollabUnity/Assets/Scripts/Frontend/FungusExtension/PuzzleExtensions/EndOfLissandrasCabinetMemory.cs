using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfLissandrasCabinetInEntrance","moves puzzle out of screen")]

public class EndOfLissandrasCabinetMemory : Command
{
    public BlockCollect blockCollect;
    public override void OnEnter()
    {
        blockCollect.SetInteractable(true);
        Continue();
    }
}
