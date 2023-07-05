using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NoteItem : MonoBehaviour
{
    public Bear puckTheBear;
    
    public GameObject interactParticle;
    private float interactRange;
    private bool isInteractable;
    private bool canCloseNote;

    [Header("object")] 
    [SerializeField] private GameObject notePlaneObj;
    [Header("positions")]
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform activePos;
    [Header("Duration")]
    [SerializeField] private float noteMovementDuration;
    private void Awake()
    {
        interactRange = 2;
        isInteractable = true;
        notePlaneObj.SetActive(false);
        canCloseNote = false;
    }

    private void Update()
    {
        if (isInteractable)
        {
            Interact();  
        }
        
        CloseNote();
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

    public void CloseNote()
    {
        if(!canCloseNote) return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            MoveNoteAway();
            Player.instance.SetCanMove(true);
            puckTheBear.isInteractable = true;
        }
    }

    void InstantiateAndMove()
    {
        canCloseNote = false;
        notePlaneObj.SetActive(true);
        notePlaneObj.transform.DOMove(activePos.position, noteMovementDuration).OnComplete(() => canCloseNote = true);
    }

    void MoveNoteAway()
    {
        notePlaneObj.transform.DOMove(initPos.position, noteMovementDuration).OnComplete(()=>
        {
            notePlaneObj.SetActive(false);
            Destroy(this);
        });
    }
}
