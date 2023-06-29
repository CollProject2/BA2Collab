using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public PlayerMemory bearMemory;
    public GameObject interactParticle;
    public int interactRange;
    public bool isInteractable;

    private void Awake()
    {
        isInteractable = false;
        interactParticle.SetActive(false); 
    }

    public void Update()
    {
        if (isInteractable)
        {
            interactParticle.SetActive(true);
            
            if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
            {
                // open HUD to give visual feedback
            

                //press E to collect
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Collect();
                }
            }
        }
    }

    void Collect()
    {
        Player.instance.RecallMemory(bearMemory);
        Player.instance.hasBear = true;
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        ItemUIManager.Instance.ToggleItem(2);
        interactParticle.SetActive(false);
        isInteractable = false;
        Destroy(gameObject);
    }

}
