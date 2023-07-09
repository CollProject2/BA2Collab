using UnityEngine;

public class BlockFaceTrigger : MonoBehaviour
{
    private PuzzleBlock puzzleBlock;
    private const string DetectorTag = "BlockFaceDetector";

    private void Start()
    {
        puzzleBlock = GetComponentInParent<PuzzleBlock>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag(DetectorTag) && puzzleBlock != null && (BlockManager.instance.currentBlock == puzzleBlock || !puzzleBlock.interactable))
        {
            puzzleBlock.SetState(name);
        }
    }

}