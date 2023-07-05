using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndofLockBoxMemory","moves puzzle out of screen")]

public class EndOfLockBoxMemory : Command
{
    public Lockbox lockBox;
    public ProjectorPuzzle projector;

    public override void OnEnter()
    {
        lockBox.MoveLockBoxAway();
        projector.SetInteractable(true);
        Continue();
    }
}
