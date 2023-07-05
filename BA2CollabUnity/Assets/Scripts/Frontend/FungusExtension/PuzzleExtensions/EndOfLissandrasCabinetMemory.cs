using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfLissandrasCabinetBlock","moves puzzle out of screen")]

public class EndOfLissandrasCabinetMemory : Command
{
    
    public override void OnEnter()
    {
        
        Continue();
    }
}
