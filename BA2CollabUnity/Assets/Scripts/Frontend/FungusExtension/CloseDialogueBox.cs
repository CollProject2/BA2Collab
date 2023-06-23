using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
[CommandInfo("DialogueBox","CloseDialogueBox","Sets position and rotation of dialogue box to closed ")]
public class CloseDialogueBox : Command
{
    public override void OnEnter()
    {
        UIManager.instance.dialogues.dialogueBox.dialogueIsPlaying = false;
        UIManager.instance.dialogues.dialogueBox.OnDialogueEnd();
        Continue();
    }
}
