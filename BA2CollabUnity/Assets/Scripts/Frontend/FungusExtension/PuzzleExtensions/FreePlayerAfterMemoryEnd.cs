using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("PLAYER","freePlayer","set canMove to true on player")]
public class FreePlayerAfterMemoryEnd : Command
{
    public override void OnEnter()
    {
        Player.instance.SetCanMove(true);
        Player.instance.isSolving = false;
        Continue();
    }
}
