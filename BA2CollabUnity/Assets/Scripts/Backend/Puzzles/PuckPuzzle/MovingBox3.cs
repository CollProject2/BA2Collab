using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBox3 : MonoBehaviour
{
    public string endingMemory;
    public float interactRange;
    public GameObject interactParticle;
    public bool isInteractable;


    private void Awake()
    {
        isInteractable = false;
    }
    public void Update()
    {
        if (isInteractable)
        {
            Interact(); 
        }
    }

    void Interact()
    {
        interactParticle.SetActive(true);

        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactParticle.SetActive(false);
                Player.instance.RecallMemory(endingMemory);
            }
        }

    }
   
}
