using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndofLockBoxMemory","moves puzzle out of screen")]

public class EndOfLockBoxMemory : Command
{
    public LockBoxItem lockBox;
    public override void OnEnter()
    {
        lockBox.MoveItemAway();
        Continue();
    }
}
