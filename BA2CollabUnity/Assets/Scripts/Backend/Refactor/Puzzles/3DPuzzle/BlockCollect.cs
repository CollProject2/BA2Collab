using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BlockCollect : MonoBehaviour
{
    public GameObject interactParticle;
    public float interactRange;
    private bool isInteractable;
    public string BlockCollectMemory;
    
    private void Update()
    {
        if (isInteractable)
        {
            Interact();  
        }
    }

    public void SetInteractable(bool state)
    {
        isInteractable = state;
    }

    public void Interact()
    {
        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            // open HUD to give visual feedback
            interactParticle.SetActive(true);
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collect();
            }
        }
    }
    
    public void Collect()
    {
        //closes HUD when activating the puzzle 
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        Player.instance.missingBlocks--;
        ItemUIManager.Instance.ToggleItem(6-Player.instance.missingBlocks);
        isInteractable = false;
        UIManager.instance.dialogues.StartDialogue(BlockCollectMemory);
        Destroy(gameObject);
    }


}
