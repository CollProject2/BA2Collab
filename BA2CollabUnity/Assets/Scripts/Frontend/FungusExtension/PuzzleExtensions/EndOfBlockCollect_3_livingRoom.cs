using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","EndOfBlockCollect_3_livingRoom","moves puzzle out of screen")]

public class EndOfBlockCollect_3_livingRoom : Command
{
    public Item newFamilyPhotoItem; 
    public override void OnEnter()
    {
        newFamilyPhotoItem.enabled = true;
        Environment.instance.trashTheVideoRoom = true;
        Continue();
    }
}
