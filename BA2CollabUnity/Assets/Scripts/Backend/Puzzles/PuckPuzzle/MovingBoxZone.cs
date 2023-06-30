using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoxZone : MonoBehaviour
{
    public PlayerMemory movingBoxDropMemory;
    public float interactRange;
    bool isPlaced;
    public GameObject interactParticle;
    private bool isInteractable;
    public GameObject movingBox;

    private void Awake()
    {
        isPlaced = false;
    }
    public void Update()
    {
        if (isInteractable)
        {
            Interact(); ;
        }
    }

    void Interact()
    {
        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !isPlaced && Player.instance.hasMovingBox)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))  
                ChangeValues();     
        }

    }
    public void PutDownBox()
    {
        Player.instance.animator.SetBool("isMoving", false);
        movingBox.transform.DOMove(transform.position, 1).OnComplete(() => ChangeValues()) ;
    }
    public void SetBoxActive()
    {
        isInteractable = true;
    }

    private void ChangeValues()
    {
        isPlaced = true;
        isInteractable = false;
        Player.instance.hasMovingBox = false;
        Player.instance.RecallMemory(movingBoxDropMemory);
    }
}
