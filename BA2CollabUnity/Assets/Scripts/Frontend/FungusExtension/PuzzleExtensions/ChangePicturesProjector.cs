using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","ChangePicture","moves puzzle out of screen")]
public class ChangePicturesProjector : Command
{
    public int picIndex;
    public ProjectorPuzzle projector;
    public override void OnEnter()
    {
        projector.ShowPicture(picIndex);
        Continue();
    }
}
