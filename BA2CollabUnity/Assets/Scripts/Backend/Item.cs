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
    
    private float distWithPlayer;
    public float interactRange = 3;
    

    private void Awake()
    {
        itemCollider = gameObject.GetComponent<Collider>();
    }

    private void Update()
    {
        CheckDistanceWithPlayer();
        Interact();
    }

    // Methods
    public void Collect()
    {
        Player.instance.CollectItem(this); // player adds item to inventory
        itemCollider.enabled = false;
        AnimateItemOnCollect();
        InteractParticle.SetActive(false);

        //UIManager.instance.puzzleUI.DisplayUIPuzzle(associatedPuzzle.puzzleID); // Open the first UI puzzle , we just added this puzzle to the inventory so -1 to have the correct puzzle index (we start at 0)
        Player.instance.SolvePuzzle(associatedPuzzle); // player attempts to solve the puzzle associated with this item
        
    }
    
    void HideItemModel()
    {
        modelObj.SetActive(true);
    }

    public void AnimateItemOnCollect()
    {
        modelObj.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(() =>
        {
            modelObj.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(HideItemModel);
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Collect();
        }
    }

    private void CheckDistanceWithPlayer()
    {
        distWithPlayer = Vector3.Distance(Player.instance.transform.position, transform.position);
    }

    private void Interact()
    {
        if (distWithPlayer < interactRange)
        {
            // open HUD to give visual feedback
            InteractParticle.gameObject.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collect();
            }
        }
        else
        {
            InteractParticle.gameObject.SetActive(false);
        }
    }

    //specific item stuff
}