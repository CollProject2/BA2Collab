using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class MoonShineItem : MonoBehaviour
{
    public GameObject interactParticle;
    public string moonshineLenternMemory;
    public float interactRange;
    private bool isInteractable;
    private bool canCloseNote;
    

    

    [Header("object")] 
    [SerializeField] private GameObject moonShineLenternItem;
    [Header("positions")]
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform activePos;
    [Header("Duration")]
    [SerializeField] private float lenternMovementDuration;
    
    

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
        moonShineLenternItem.SetActive(true);
        moonShineLenternItem.transform.DOScale(new Vector3(3.5f, 3.5f, 3.5f), lenternMovementDuration);
        moonShineLenternItem.transform.DOMove(activePos.position, lenternMovementDuration).OnComplete(() =>
        {
            UIManager.instance.dialogues.StartDialogue(moonshineLenternMemory);
            
        });
    }

    public void MoveMoonshineLenternAway()
    {
        moonShineLenternItem.transform.DOScale(new Vector3(1, 1, 1), lenternMovementDuration);
        moonShineLenternItem.transform.DOMove(initPos.position, lenternMovementDuration).OnComplete(()=>
        {
            LightManager.instance.OpenLivingRoomEntranceDoorHighLight(true);
            
        });
    }

   
}
