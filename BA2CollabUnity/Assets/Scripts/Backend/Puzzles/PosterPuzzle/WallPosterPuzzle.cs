using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPosterPuzzle : MonoBehaviour
{
    public string posterGapMemory;
    public float interactRange;
    public bool isInteractable;

    public void Update()
    {
        if (!isInteractable) return;

        Interact();
    }

    void Interact()
    {
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving && Player.instance.hasPoster)
        {
            // open HUD to give visual feedback

            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<MeshRenderer>().enabled = true;
                isInteractable = false;
                Player.instance.SetCanMove(false);
                Player.instance.animator.SetBool("isMoving", false);
                Player.instance.RecallMemory(posterGapMemory);
            }
        }
    }
}
