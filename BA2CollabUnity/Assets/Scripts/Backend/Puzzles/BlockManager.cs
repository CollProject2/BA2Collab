using Fungus;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
    public PuzzleBlock currentBlock;

    public PuzzleBlock[] gridBlocks = new PuzzleBlock[9]; // 3x3 grid flattened to 1D
    //Singelton instance
    public static BlockManager instance = null;

    private void Awake()
    {
        //Singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    public void SetCurrentBlock(PuzzleBlock block)
    {
        currentBlock = block;
    }

    private bool CheckWinCondition(BlockFace blockFace)
    {
        foreach (PuzzleBlock block in gridBlocks)
        {
           if (block.CurrentFace != blockFace) return false;
        }
        Debug.Log("trigger memmory yellow");
        return true;
    }

    public void CallCheck()
    {
        CheckWinCondition(BlockFace.Top);
    }

    public void RotateBlockAt(int index, RotationDirection direction)
    {
        if (index >= 0 && index < gridBlocks.Length) // Check that index is within array bounds
        {
            gridBlocks[index].RotateBlock(direction);
        }
    }
}

