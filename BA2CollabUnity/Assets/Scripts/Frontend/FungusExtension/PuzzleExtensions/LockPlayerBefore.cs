using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("PLAYER", "lockPlayer", "set canMove to false on player")]
public class LockPlayerBefore : Command
{
    public override void OnEnter()
    {
        Player.instance.SetCanMove(false);
        Player.instance.isSolving = true;
        Continue();
    }
}