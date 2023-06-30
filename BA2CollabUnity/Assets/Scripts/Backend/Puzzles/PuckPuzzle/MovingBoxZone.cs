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
    public GameObject movingBox;
    

    private void Awake()
    {
        isPlaced = false;
    }
    public void Update()
    {
        if (Player.instance.hasMovingBox)
        {
            Interact(); ;
        }
    }

    void Interact()
    {
        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !isPlaced)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                ChangeValues();  
                PutDownBox();
            }
        }
    }
    public void PutDownBox()
    {
        Player.instance.animator.SetBool("isMoving", false);
        Player.instance.hasMovingBox = false;
        interactParticle.SetActive(false);
        movingBox.transform.DOMove(transform.position, 1).OnComplete(() =>
        {
            movingBox.transform.parent = null;
            ChangeValues();
        }) ;
    }
    

    private void ChangeValues()
    {
        isPlaced = true;
        Player.instance.RecallMemory(movingBoxDropMemory);
    }
}
