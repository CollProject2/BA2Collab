using System;
using UnityEngine;
using DG.Tweening;
using static UnityEditor.Progress;

public class Item3DPuzzle : InteractableItem
{
    // Properties
    public Puzzle associatedPuzzle;
    
    public override void Collect()
    {
        //disable movement when solving
        Player.instance.SetCanMove(false);
        //we solve
        Player.instance.isSolving = true;
        //set animation
        Player.instance.animator.SetTrigger("pickUp");
        interactParticle.SetActive(false);
        //solves the puzzle associated with the item
        SolvePuzzle(associatedPuzzle); // player attempts to solve the puzzle associated with this item
    }
    public void SolvePuzzle(Puzzle puzzle)
    {
        // instantiate the puzzle prefab
        // if player is in garden change puzzle spawn and active pos
        if (!Player.instance.inGarden)
        {
            puzzle.StartPuzzle(puzzle, UIManager.instance.puzzleUI.blockPuzzleInstantiatePos);
        }
        else if (Player.instance.inGarden)
        {
            puzzle.StartPuzzle(puzzle, UIManager.instance.puzzleUI.gardenBlockPuzzleInstantiatePos);
        }

    }
    public void DestroyItemScript()
    {
        Destroy(this);
    }
}