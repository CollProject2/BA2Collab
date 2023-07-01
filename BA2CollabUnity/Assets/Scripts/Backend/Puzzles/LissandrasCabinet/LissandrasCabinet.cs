using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LissandrasCabinet : MonoBehaviour
{
    public GameObject interactParticle;
    public GameObject CubeObject;
    public float interactRange;
    public bool isInteractable;


    public string cabinetMemory;
    

    [Header("object")] 
    [SerializeField] private GameObject cabinetDoor;
    [Header("positions")]
    [SerializeField] private Transform cubeActivePos;
    [SerializeField] private Transform cabinetDoorActivePos;
    [Header("Duration")]
    [SerializeField] private float cabinetDoorMovementDuration;
    
    

    public void SetInteractable(bool state)
    {
        isInteractable = state;
    }

    private void Update()
    {
        if (isInteractable)
        {
            Interact();  
        }
    }

    public void Interact()
    {
        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            // open HUD to give visual feedback
            interactParticle.SetActive(true);
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
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);

        isInteractable = false;
    }

    

    void InstantiateAndMove()
    {
        cabinetDoor.SetActive(true);
        cabinetDoor.transform.DOMove(cabinetDoorActivePos.position, cabinetDoorMovementDuration).OnComplete(() =>
        {
            UIManager.instance.dialogues.StartDialogue(cabinetMemory);
            CubeObject.transform.DOMove(cubeActivePos.position, 1);

        });
    }

    

    
}
