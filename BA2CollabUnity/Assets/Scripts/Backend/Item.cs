using System;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    // Properties
    public string itemName { get; private set; }
    public Puzzle associatedPuzzle;

    public GameObject modelObj;
    public GameObject InteractParticle;
    
    public float interactRange;
    public bool isHidden;
    
    private void Update()
    {
        //update distance
        if(!isHidden)
                Interact();
    }

    // Methods
    //Interact with the player
    private void Interact()
    {
        InteractParticle.SetActive(true);
        //if the player is in range of the item, not solving a puzzle and the puzzle is not hidden
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            // open HUD to give visual feedback
           
            
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collect();
            }
        }
    }
    public void Collect()
    {
        //adds item to player inventory
        Player.instance.CollectItem(this);
        //closes HUD when activating the puzzle 
        InteractParticle.SetActive(false);
        //solves the puzzle associated with the item
        Player.instance.SolvePuzzle(associatedPuzzle); // player attempts to solve the puzzle associated with this item
        SetIsHidden(true);
        
    }
   
    public void SetIsHidden(bool state)
    {
        isHidden = state;
    }


}