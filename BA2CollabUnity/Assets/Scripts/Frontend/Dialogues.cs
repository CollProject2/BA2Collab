using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Dialogues : MonoBehaviour
{
    // Properties
    public Flowchart MemoryUnlocked_1;

    public List<string> memoryNames;


    // executes the block inside the given flowchart
    public void StartDialogue(int memoryNum)
    {
        MemoryUnlocked_1.ExecuteBlock(memoryNames[memoryNum]); // string dependency
    }
}
