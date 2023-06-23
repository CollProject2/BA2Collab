using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;
[CommandInfo("DialogueBox","OpenDialogueBox","Sets position and rotation of dialogue box to open ")]
public class OpenDialogueBox : Command
{
    public override void OnEnter()
    {
        UIManager.instance.dialogues.dialogueBox.dialogueIsPlaying = true;
        UIManager.instance.dialogues.dialogueBox.MoveToPlayingTextPos();
        Continue();
    }
}
