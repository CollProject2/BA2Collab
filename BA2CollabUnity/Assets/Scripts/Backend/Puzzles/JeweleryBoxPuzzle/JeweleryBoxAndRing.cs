using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JeweleryBoxAndRing : MonoBehaviour
{
    public GameObject interactParticle;
    public float interactRange;
    public bool isInteractable;
    public string jeweleryMemory;


    [Header("object")]
    [SerializeField] private GameObject JeweleryPuzzleObj;

    [SerializeField] private GameObject jeweleryBoxPivot;
    [SerializeField] private GameObject Ring;
    [Header("positions")]
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform activePos;
    [SerializeField] private Transform ringInitPos;
    [SerializeField] private Transform ringActivePos;
    [Header("Duration")]
    [SerializeField] private float jeweleryMoveDuration;
    [SerializeField] private float jeweleryBoxOpenDur;

    private void Update()
    {
        if (!isInteractable) return;

        Interact();
        

    }

    public void Interact()
    {

        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
                Collect();
        }
    }

    public void Collect()
    {
        //closes HUD when activating the puzzle 
        isInteractable = false;
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        
    }

    void InstantiateAndMove()
    {
        JeweleryPuzzleObj.transform.DOMove(activePos.position, jeweleryMoveDuration);
        JeweleryPuzzleObj.transform.DOScale(new Vector3(8.7f, 8.7f, 8.7f), jeweleryBoxOpenDur);
        JeweleryPuzzleObj.transform.DORotate(new Vector3(-12, 0, 0), jeweleryMoveDuration).OnComplete(() =>
        {
            jeweleryBoxPivot.transform.DOLocalRotate(new Vector3(0, 0, -96), jeweleryBoxOpenDur).OnComplete(() =>
            {
                Ring.transform.DOLocalRotate(new Vector3(0, 540, 0),jeweleryMoveDuration, RotateMode.FastBeyond360);
                Ring.transform.DOMove(ringActivePos.position, jeweleryMoveDuration/2).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    UIManager.instance.dialogues.StartDialogue(jeweleryMemory);
                });
            });
        });
    }

    public void MoveMedallionAway()
    {

        Ring.transform.DOMove(ringInitPos.position, jeweleryMoveDuration).OnComplete(() =>
        {
            jeweleryBoxPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), jeweleryMoveDuration);
            JeweleryPuzzleObj.transform.DOMove(initPos.position, jeweleryMoveDuration);
            JeweleryPuzzleObj.transform.DOScale(new Vector3(1, 1, 1), jeweleryBoxOpenDur);
            JeweleryPuzzleObj.transform.DORotate(new Vector3(0, 0, 0), jeweleryMoveDuration);
        });

    }
}
