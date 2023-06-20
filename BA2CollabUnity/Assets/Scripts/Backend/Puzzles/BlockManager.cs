using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class BlockManager : MonoBehaviour
{
    public PuzzleBlock currentBlock;
    public PuzzleBlock[] gridBlocks = new PuzzleBlock[9]; // 3x3 grid flattened to 1D
    //Singelton instance
    public static BlockManager instance = null;

    public float delayAfterPuzzleEnd = 2.5f;

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
        DisplayNextBlock();
    }

    public void ActivateBlocks()
    {
        foreach (var block in gridBlocks)
        {
            block.interactable = true;
        }
    }

    public void DeactivateBlocks()
    {
        foreach (var block in gridBlocks)
        {
            block.interactable = false;
        }
    }

    public void DisplayNextBlock()
    {
        switch (Player.instance.missingBlocks)
        {
            case 0:
                gridBlocks[0].gameObject.SetActive(true);
                gridBlocks[1].gameObject.SetActive(true);
                gridBlocks[2].gameObject.SetActive(true);
                break;
            case 1:
                gridBlocks[1].gameObject.SetActive(true);
                gridBlocks[0].gameObject.SetActive(true);
                break;
            case 2:
                gridBlocks[0].gameObject.SetActive(true);
                break;
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
            if (block.isActiveAndEnabled)
            {
                if (block.CurrentFace != blockFace)
                    return false;
            }
        }
        return true;
    }

    public void CallCheck()
    {
        if (CheckWinCondition(BlockFace.Top)) // give wincon
        {
            Player.instance.RecallMemory();
            DeactivateBlocks();
            currentBlock.transform.DOMove(currentBlock.defaultBlockPos.position, 1);
            Invoke("OnPuzzleFinishedMove", delayAfterPuzzleEnd);
        }
            
    }

    private void OnPuzzleFinishedMove()
    {
        gameObject.transform
            .DOMove(UIManager.instance.puzzleUI.blockPuzzleInstantiatePos.position,
                UIManager.instance.puzzleUI.blockPuzzleMoveDur).SetEase(UIManager.instance.puzzleUI.blockPuzzleCurve).OnComplete(()=> Destroy(gameObject));
    }

    public void RotateBlockAt(int index, RotationDirection direction)
    {
        if (index >= 0 && index < gridBlocks.Length) // Check that index is within array bounds
        {
            gridBlocks[index].RotateBlock(direction);
        }
    }
}

