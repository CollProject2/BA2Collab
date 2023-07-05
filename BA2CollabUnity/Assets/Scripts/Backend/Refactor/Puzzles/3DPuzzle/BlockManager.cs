using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public PuzzleBlock currentBlock;
    private Puzzle puzzle;
    public List<PuzzleBlock> gridBlocks = new();
    public static BlockManager instance = null;
    public bool isInteractable;
    public BlockFace wincon;
    public bool isComplete;

    public Quaternion targetRotation = Quaternion.Euler(270, 0, 0);
    public float delayAfterPuzzleEnd = 2.5f;
    private bool solved;

    private Dictionary<int, int[]> blockDisplayMapping = new Dictionary<int, int[]>()
    {
        { 0, new int[] {0, 1, 2} },
        { 1, new int[] {0, 1} },
        { 2, new int[] {0} }
    };

    private void Awake()
    {
        //singleton
        if (instance == null)

            instance = this;
        else
            Destroy(this);

        puzzle = GetComponentInParent<Puzzle>();
        DisplayNextBlock();
        solved = false;
    }
    public bool IsComplete()
    {
        return isComplete;
    }
    //set this as the current block
    public void SetCurrentBlock(PuzzleBlock block)
    {
        currentBlock = block;
    }
    public void OnButtonClickBlock(int direction)
    {
        if (currentBlock != null)
            RotateBlockAt(currentBlock.index, (RotationDirection)direction);
    }
    private void Update()
    {
        if (!isInteractable) return;
        CheckInput();

    }
    private void CheckInput()
    {
        //check if the player pressed the respective key
        if (Input.GetKeyDown(KeyCode.A))
            OnButtonClickBlock(2);
        if (Input.GetKeyDown(KeyCode.D))
            OnButtonClickBlock(3);
        if (Input.GetKeyDown(KeyCode.W))
            OnButtonClickBlock(0);
        if (Input.GetKeyDown(KeyCode.S))
            OnButtonClickBlock(1);
    }

    //make the blocks clickable
    public void ActivateBlocks()
    {
        gridBlocks.ForEach(block => block.interactable = true);
    }

    //make the blocks unclickable
    public void DeactivateBlocks()
    {
        gridBlocks.ForEach(block => block.interactable = false);
        isInteractable = false;
    }

    //display the next block if we collected a block
    public void DisplayNextBlock()
    {
        //respective key with respective objects that need to be displayed
        if (blockDisplayMapping.ContainsKey(Player.instance.missingBlocks))
        {
            ItemUIManager.Instance.ToggleItem(6 - Player.instance.missingBlocks);
            //block array that has the map with the key
            int[] blockIndexes = blockDisplayMapping[Player.instance.missingBlocks];

            foreach (int index in blockIndexes)
                gridBlocks[index].gameObject.SetActive(true);

        }
    }


    //check wincon and finish up the puzzle
    public void CallCheck()
    {
        if (CheckWinCondition() && !solved)
        {
            //disable block interaction
            DeactivateBlocks();
            //recall memory
            Player.instance.RecallMemory(puzzle.associatedMemory);
            //move the last block in place
            currentBlock.transform.DOMove(currentBlock.defaultBlockPos.position, 1);
            solved = true;
            isComplete = true;
            StoryManager.instance.AdvanceGameState();
        }
    }

    private bool CheckWinCondition()
    {
        foreach (PuzzleBlock block in gridBlocks)
        {
            if (block.isActiveAndEnabled)
            {
                if ((block.CurrentFace != wincon || !block.rightRotation))
                    return false;
            }
        }
        return true;
    }


    //move the puzzle away
    public void OnPuzzleFinishedMove()
    {
        gameObject.transform.DOMove(UIManager.instance.puzzleUI.blockPuzzleInstantiatePos.position, UIManager.instance.puzzleUI.blockPuzzleMoveDur)
            .SetEase(UIManager.instance.puzzleUI.blockPuzzleCurve)
            .OnComplete(() => Destroy(gameObject));
    }

    //rotate the block with the given index in the list
    public void RotateBlockAt(int index, RotationDirection direction)
    {
        if (index >= 0 && index < gridBlocks.Count)
            gridBlocks[index].RotateBlock(direction);

    }
    public void DestroyPuzzle()
    {
        Destroy(gameObject);
    }

    internal void Activate()
    {
        isInteractable = true;
    }
}
