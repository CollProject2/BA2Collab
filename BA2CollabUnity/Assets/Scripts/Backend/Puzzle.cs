using DG.Tweening;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    //Properties
    public bool isSolved { get; set; }
    public string puzzleName { get; private set; }
    public int puzzleID { get; private set; }
    public PlayerMemory associatedMemory;

    [CanBeNull] public GameObject puzzleObj;

    //Init
    private void Awake()
    {
        isSolved = false;
    }
    public void StartPuzzle(Puzzle associatedPuzzle, Transform puzzleSpawn)
    {
        puzzleObj = Instantiate(associatedPuzzle,puzzleSpawn.position,puzzleSpawn.rotation).gameObject;
        puzzleObj.transform
            .DOMove(UIManager.instance.puzzleUI.blockPuzzleActivePos.position, UIManager.instance.puzzleUI.blockPuzzleMoveDur)
            .SetEase(UIManager.instance.puzzleUI.blockPuzzleCurve);
        
        //startPuzzle solving Music from AudioManager, get the event ref from FMODEvents
        AudioManager.instance.InitializeMemoryMusic(FMODEvents.instance.memoryMusic_1);
    }

}
