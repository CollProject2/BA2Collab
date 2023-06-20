using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleButton : MonoBehaviour
{
    public void OnButtonClickBlock(int direction)
    {
        if(BlockManager.instance.currentBlock != null)
            BlockManager.instance.RotateBlockAt(BlockManager.instance.currentBlock.index, (RotationDirection)direction);
    }
}
