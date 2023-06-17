using Unity.VisualScripting;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    //Properties
    public bool isSolved { get; set; }
    public string puzzleName { get; private set; }
    public int puzzleID { get; private set; }
    public PlayerMemory associatedMemory;

    //Init
    private void Awake()
    {
        isSolved = false;
    }
    public void StartPuzzle(Puzzle associatedPuzzle)
    {
        Instantiate(associatedPuzzle);
        
        //startPuzzle solving Music from AudioManager, get the event ref from FMODEvents
        AudioManager.instance.InitializeMemoryMusic(FMODEvents.instance.memoryMusic_1);
    }

}
