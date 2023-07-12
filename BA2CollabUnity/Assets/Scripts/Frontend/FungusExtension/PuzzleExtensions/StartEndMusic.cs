using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle","StartEndMusic","moves puzzle out of screen")]
public class StartEndMusic : Command
{
    public override void OnEnter()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.yoursEverMusic,Vector3.zero);
        Continue();
    }
}
