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
        // Liwa: The memory of the linked associated item is the last one in the list
        UIManager.instance.dialogues.StartDialogue(memoryName);
    }
    //specific memory stuff

}