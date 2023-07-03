using UnityEngine;

public class BlockFaceTrigger : MonoBehaviour
{
    private PuzzleBlock puzzleBlock;
    private const string DetectorTag = "BlockFaceDetector";

    private void Start()
    {
        puzzleBlock = GetComponentInParent<PuzzleBlock>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(DetectorTag))
        {
            puzzleBlock.SetState(name);
            puzzleBlock.SetCurrentConditions();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(DetectorTag))
        {
            puzzleBlock.SetCurrentConditions();
        }
    }
}
