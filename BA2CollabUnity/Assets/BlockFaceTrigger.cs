using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFaceTrigger : MonoBehaviour
{
    PuzzleBlock puzzleBlock;

    private bool hasBeenTriggered;

    // Start is called before the first frame update
    void Start()
    {
        hasBeenTriggered = false;
        puzzleBlock = GetComponentInParent<PuzzleBlock>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BlockFaceDetector") && !hasBeenTriggered)
        {
            puzzleBlock.SetState(this.name);
            hasBeenTriggered = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        hasBeenTriggered = false;
    }
}
