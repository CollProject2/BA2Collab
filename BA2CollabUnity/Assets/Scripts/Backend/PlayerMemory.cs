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
        UIManager.instance.dialogues.StartDialogue(1);
    }
    //specific memory stuff

}