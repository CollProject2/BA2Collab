using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("Animations","CharacterAnim", "trigger Character Anim")]
public class AnimTriggerFungus : Command
{
    public enum Anim
    {
        pickUp,
        other,
    }

    public Anim animOption;
    public override void OnEnter()
    {
        Player.instance.animator.SetTrigger(animOption.ToString());
        Continue();
    }
}
