using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Dialogues : MonoBehaviour
{
    // Properties
    public Flowchart MemoryUnlocked_1;
    public DialogueBox dialogueBox;

    // executes the block inside the given flowchart
    public void StartDialogue(string memoryName)
    {
        MemoryUnlocked_1.ExecuteBlock(memoryName); // string dependency
    }
}
