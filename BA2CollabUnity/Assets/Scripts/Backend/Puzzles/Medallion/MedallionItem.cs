using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MedallionItem : MonoBehaviour
{
    public GameObject interactParticle;
    public float interactRange;
    private bool canCloseMedallion;
    private bool isInteractable;
    public string medallionMemory;


    [Header("object")] 
    [SerializeField] private GameObject medallionPuzzleObj;

    [SerializeField] private GameObject medallionPivot;
    [SerializeField] private GameObject letter;
    [Header("positions")]
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform activePos;
    [Header("Duration")]
    [SerializeField] private float medallionMovementDuration;
    [SerializeField] private float medallionOpenDuration;
    private void Awake()
    {
        canCloseMedallion = false;

    }
    
    private void Update()
    {
        if (!isInteractable) return;

        Interact();
        
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
        canCloseMedallion = false;
        medallionPuzzleObj.SetActive(true);
        medallionPuzzleObj.transform.DOMove(activePos.position, medallionMovementDuration).OnComplete(() => canCloseMedallion = true);
        medallionPuzzleObj.transform.DOScale(new Vector3(8.7f, 8.7f, 8.7f), medallionOpenDuration);
        medallionPuzzleObj.transform.DORotate(new Vector3(0, -90, 0), medallionMovementDuration).OnComplete(() =>
        {
            medallionPivot.transform.DORotate(new Vector3(0, -90, 96), medallionOpenDuration).OnComplete(() =>
            {
                letter.SetActive(true);
                UIManager.instance.dialogues.StartDialogue(medallionMemory);
                
            });
        });
    }

    public void MoveMedallionAway()
    {
        letter.SetActive(false);
        medallionPivot.transform.DORotate(new Vector3(0, -90, 1), medallionOpenDuration).OnComplete(() =>
        {
            medallionPuzzleObj.transform.DOMove(initPos.position, medallionMovementDuration);
            medallionPuzzleObj.transform.DOScale(new Vector3(1, 1, 1), medallionOpenDuration);
            medallionPuzzleObj.transform.DORotate(new Vector3(0, -60, -90), medallionMovementDuration);
        });
        
    }
    
}
