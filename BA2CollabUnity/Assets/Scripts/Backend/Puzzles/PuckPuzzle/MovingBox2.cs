using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox2 : MonoBehaviour
{
    public PlayerMemory movingBox2Memory;
    public int interactRange;
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

        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !isPlaced && Player.instance.hasMovingBox)
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
    }

    public void SetBoxActive()
    {
        isInteractable = true;
    }

    private void ChangeValues()
    {
        isPlaced = true;
        isInteractable = false;
        Player.instance.RecallMemory(movingBox2Memory);
    }
}
