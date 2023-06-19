using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("Fmod","SetTextSfxAge","Change Text Sound Age Parameter, changes sound, TextSoundAge")]
public class FmodFungusSetPlayerTextAge : Command
{
    public int ageToSet;
    public override void OnEnter()
    {
        AudioManager.instance.SetTextSoundAge(ageToSet);
        Continue();
    }
}
