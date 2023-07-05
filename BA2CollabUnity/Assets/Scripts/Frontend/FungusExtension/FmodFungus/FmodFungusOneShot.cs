using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using FMODUnity;

[CommandInfo("Fmod","OneShotSFX","Play a OneShot sound effect on enter")]
public class FmodFungusOneShot : Command
{
    [SerializeField] private EventReference testSound;
    
    public override void OnEnter()
    {
        AudioManager.instance.PlayOneShot(testSound, this.transform.position);
        Continue();
    }
}
