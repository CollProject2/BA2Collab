using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GlassesItem : MonoBehaviour
{
    public GameObject interactParticle;
    public Glasses glasses;
    public Lockbox lockBox;
    public GameObject glassesModel;
    public float interactRange;
    public bool isInteractable;

    

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
            {
                Collect();
            }
        }
    }
    
    public void Collect()
    {
        //closes HUD when activating the puzzle 
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        ActivateGlassesOnCamera();
        isInteractable = false;
        CameraManager.instance.CameraZoomInScreeningRoom();
    }

    public void ActivateGlassesOnCamera()
    {
        glassesModel.SetActive(true);
        Player.instance.hasGlasses = true;
        lockBox.isInteractable = true;
        glassesModel.transform.DOLocalMove(new Vector3(0, -1, 3), 1).OnComplete(() =>
        {
            glassesModel.transform.DOLocalMove(new Vector3(0, -1, 0.16f), 1).OnComplete(() =>
            {
                glassesModel.transform.DOLocalMove(new Vector3(0, -1, -1), 1);
                glasses.gameObject.SetActive(true);
                glasses.InitializeGlasses();
            });
        });
        
    }

    
}
