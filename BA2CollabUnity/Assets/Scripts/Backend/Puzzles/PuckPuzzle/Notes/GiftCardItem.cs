using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GiftCardItem : MonoBehaviour
{
    public GameObject interactParticle;
    public float interactRange;
    private bool isInteractable;
    private bool canCloseNote;

    public string giftCardMemory;

    [Header("object")] 
    [SerializeField] private GameObject giftCard;
    [Header("positions")]
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform activePos;
    [Header("Duration")]
    [SerializeField] private float noteMovementDuration;
    private void Awake()
    {
        
        canCloseNote = false;
    }

    public void SetInteractable(bool state)
    {
        isInteractable = state;
    }

    private void Update()
    {
        if (isInteractable)
        {
            Interact();  
        }
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
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);

        isInteractable = false;
    }

    

    void InstantiateAndMove()
    {
        canCloseNote = false;
        giftCard.SetActive(true);
        giftCard.transform.DOMove(activePos.position, noteMovementDuration).OnComplete(() =>
        {
            UIManager.instance.dialogues.StartDialogue(giftCardMemory);
            canCloseNote = true;
        });
    }

    public void MoveNoteAway()
    {
        giftCard.transform.DOMove(initPos.position, noteMovementDuration).OnComplete(()=>
        {
            giftCard.SetActive(false);
            Destroy(this);
        });
    }
}
