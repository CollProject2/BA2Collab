using UnityEngine;

public class PlayerMemory : MonoBehaviour
{
    //Properties
    public string memoryName { get; private set; }
    public bool isUnlocked { get; set; }

    //Methods
    public void InitializePlayerMemory(string name)
    {
        memoryName = name;
        isUnlocked = false;
        gameObject.SetActive(false);
    }

    public void Unlock()
    {
        isUnlocked = true;
        gameObject.SetActive(true);
        
        // TEST, memory objects can also hold their own flowcharts, for now I put them together on a manager.
        // Liwa: The memory of the linked associated item is the last one in the list
        UIManager.instance.dialogues.StartDialogue(Player.instance.inventory[^1].associatedPuzzle.puzzleID);
        gameObject.SetActive(false);
    }
    //specific memory stuff

}