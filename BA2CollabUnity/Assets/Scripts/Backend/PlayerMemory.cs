using UnityEngine;

public class PlayerMemory : MonoBehaviour
{
    //Properties
    public string memoryName;
    public bool isUnlocked { get; set; }

    //Methods
    public void Unlock()
    {
        isUnlocked = true;
        // TEST, memory objects can also hold their own flowcharts, for now I put them together on a manager.
        UIManager.instance.dialogues.StartDialogue(memoryName);
        
        // when it is Unlocked MemoryMusic parameter changes and switch to to the endpart
        AudioManager.instance.SetMemoryParameter("PuzzleSolvingState",1);
    }
}