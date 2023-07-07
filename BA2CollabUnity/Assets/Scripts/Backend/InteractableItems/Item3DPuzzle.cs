using System;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Progress;

public class Item3DPuzzle : InteractableItem
{
    // Properties
    public Puzzle associatedPuzzle;
    
    protected override void Collect()
    {
        base.Collect();
        //we solve
        Player.instance.isSolving = true;
        //set animation
        Player.instance.animator.SetTrigger("pickUp");
        //solves the puzzle associated with the item
        SolvePuzzle(associatedPuzzle); // player attempts to solve the puzzle associated with this item
        SetIsComplete(true);
    }

    public void SolvePuzzle(Puzzle puzzle)
    {
        // instantiate the puzzle prefab
        // if player is in garden change puzzle spawn and active pos
        if (!Player.instance.inGarden)
            puzzle.StartPuzzle(puzzle, UIManager.instance.puzzleUI.blockPuzzleInstantiatePos);
        else if (Player.instance.inGarden)
            puzzle.StartPuzzle(puzzle, UIManager.instance.puzzleUI.gardenBlockPuzzleInstantiatePos);
    }

}