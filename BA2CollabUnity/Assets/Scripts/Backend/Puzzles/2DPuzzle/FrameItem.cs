using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FrameItem : MonoBehaviour
{
    public GameObject interactParticle;
    private float interactRange;
    private bool canCloseFrame;
    private bool isInteractable;


    [Header("object")] 
    [SerializeField] private GameObject framePuzzleObj;
    [Header("positions")]
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform activePos;
    [Header("Duration")]
    [SerializeField] private float framePuzzleMovementDuration;
    private void Awake()
    {
        interactRange = 0.5f;
        framePuzzleObj.SetActive(false);
        canCloseFrame = false;
        isInteractable = true;

    }

    private void Update()
    {
        if (!Player.instance.hasBear || !isInteractable) return;

        Interact();
        
    }

    public void Interact()
    {
        
        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
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
        Puzzle2DManager.instance.isInteractable = true;
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        isInteractable = false;
    }

    void InstantiateAndMove()
    {
        canCloseFrame = false;
        framePuzzleObj.SetActive(true);
        framePuzzleObj.transform.DOMove(activePos.position, framePuzzleMovementDuration).OnComplete(() => canCloseFrame = true);
    }

    public void Move2DPuzzleAway()
    {
        framePuzzleObj.transform.DOMove(initPos.position, framePuzzleMovementDuration).OnComplete(()=>
        {
            framePuzzleObj.SetActive(false);
            Destroy(this);
        });
    }
}
