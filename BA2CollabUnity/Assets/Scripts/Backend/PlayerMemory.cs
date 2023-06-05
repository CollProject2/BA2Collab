using UnityEngine;

public class PlayerMemory: MonoBehaviour
{
    //Properties
    public string memoryName { get; private set; }
    public Puzzle associatedPuzzle { get; private set; }
    public bool isUnlocked { get; private set; }

    //Constructor
    public PlayerMemory(string name, Puzzle puzzle)
    {  
       memoryName = name;
       associatedPuzzle = puzzle;
       isUnlocked = false;
    }

    //Methods
    public void Unlock()
    {
        isUnlocked = true;
    }

    public void Recall()
    {
        Player.instance.RecallMemory(this);
    }
}