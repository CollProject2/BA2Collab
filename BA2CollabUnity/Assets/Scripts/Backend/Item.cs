using System;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    // Properties
    public string itemName { get; private set; }
    public Puzzle associatedPuzzle;
    private Collider itemCollider;
    
    public GameObject modelObj;
    public GameObject InteractParticle;
    
    public float interactRange = 3;
    private bool isHidden;
    

    private void Awake()
    {
        itemCollider = gameObject.GetComponent<Collider>();
        isHidden = false;
    }

    private void Update()
    {
        //update distance
        Interact();
    }

    // Methods
    public void Collect()
    {
        //adds item to player inventory
        Player.instance.CollectItem(this); 
        //disables collider
        itemCollider.enabled = false;
        //animates item on collect
        AnimateItemOnCollect();
        //closes HUD when activating the puzzle 
        InteractParticle.SetActive(false);
        //solves the puzzle associated with the item
        Player.instance.SolvePuzzle(associatedPuzzle); // player attempts to solve the puzzle associated with this item
        
    }
    
    //set model inactive
    void HideItemModel()
    {
        modelObj.SetActive(true);
    }

    //animate item on collect
    public void AnimateItemOnCollect()
    {
        modelObj.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(() =>
        {
            modelObj.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(HideItemModel);
            isHidden = true;
        });
    }

    //Interact with the player
    private void Interact()
    {
        //if the player is in range of the item, not solving a puzzle and the puzzle is not hidden
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving && !isHidden)
        {
            // open HUD to give visual feedback
            InteractParticle.SetActive(true);
            
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collect();
            }
        }
        else
        {
            // close HUD
            InteractParticle.SetActive(false);
        }
    }

}