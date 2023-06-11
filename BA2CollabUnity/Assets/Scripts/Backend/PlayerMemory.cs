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
        
        
        UIManager.instance.dialogues.StartDialogue(0);
    }
    //specific memory stuff

}