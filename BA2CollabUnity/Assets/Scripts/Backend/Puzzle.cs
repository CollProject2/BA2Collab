using DG.Tweening;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    //Properties
    public string puzzleName { get; private set; }
    public int puzzleID { get; private set; }
    public string associatedMemory;

    [CanBeNull] private GameObject puzzleObj;


    public void StartPuzzle(Puzzle associatedPuzzle, Transform puzzleSpawn)
    {
        //spawn the puzzle prefab
        puzzleObj = Instantiate(associatedPuzzle,puzzleSpawn.position,puzzleSpawn.rotation).gameObject;
        //tween it in the UI
        StoryManager.instance.blockManager = puzzleObj.GetComponentInChildren<BlockManager>();

        if (!Player.instance.inGarden)
        {
            puzzleObj.transform
                .DOMove(UIManager.instance.puzzleUI.blockPuzzleActivePos.position, UIManager.instance.puzzleUI.blockPuzzleMoveDur)
                .SetEase(UIManager.instance.puzzleUI.blockPuzzleCurve).OnComplete(()=> BlockManager.instance.ActivateBlocks());
        }
        else if (Player.instance.inGarden)
        {
            puzzleObj.transform
                .DOMove(UIManager.instance.puzzleUI.gardenBlockPuzzleActivePos.position, UIManager.instance.puzzleUI.blockPuzzleMoveDur)
                .SetEase(UIManager.instance.puzzleUI.blockPuzzleCurve).OnComplete(()=> BlockManager.instance.ActivateBlocks());
        }
        
        
        //startPuzzle solving Music from AudioManager, get the event ref from FMODEvents
        AudioManager.instance.InitializeMemoryMusic(FMODEvents.instance.memoryMusic_1);
    }

}
