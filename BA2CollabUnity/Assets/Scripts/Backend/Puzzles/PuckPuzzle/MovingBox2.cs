using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MovingBox2 : MonoBehaviour
{
    public string PickUpBoxAndBearMemory;
    public float interactRange;
    bool isPlaced;
    public GameObject interactParticle;
    private bool isInteractable;


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

        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !isPlaced && !Player.instance.hasMovingBox)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeValues();
                PickUpBox();
            }
        }

    }
    private void PickUpBox()
    {
        //pick up box stuff 
        // change player anim 
        // move the box or make it invisible and then put it on player
        interactParticle.SetActive(false);
        LightManager.instance.OpenOfficeMovingBoxHighLight(false);
        Player.instance.SetCanMove(false);
        transform.DOMove(Player.instance.playerCarrypos.position, 1).OnComplete(
            () =>
            {
                transform.parent = Player.instance.transform;
                Player.instance.SetCanMove(true);
            });
        
    }

    public void SetBoxActive()
    {
        isInteractable = true;
    }

    private void ChangeValues()
    {
        isPlaced = true;
        isInteractable = false;
        Player.instance.hasMovingBox = true;
        Player.instance.RecallMemory(PickUpBoxAndBearMemory);
    }
}
