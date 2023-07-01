using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfLissandrasCabinetBlock","moves puzzle out of screen")]

public class EndOfLissandrasCabinetMemory : Command
{
    public TheatreArticleItem theatreArticleItem;
    public override void OnEnter()
    {
        theatreArticleItem.enabled = true;
        Continue();
    }
}
