using UnityEngine;

public class BlockFaceTrigger : MonoBehaviour
{
    private PuzzleBlock puzzleBlock;
    private bool hasBeenTriggered = false;
    private const string DetectorTag = "BlockFaceDetector";

    private void Start()
    {
        puzzleBlock = GetComponentInParent<PuzzleBlock>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(DetectorTag) && !hasBeenTriggered)
        {
            puzzleBlock.SetState(name);
            hasBeenTriggered = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(DetectorTag))
        {
            hasBeenTriggered = false;
        }
    }
}
