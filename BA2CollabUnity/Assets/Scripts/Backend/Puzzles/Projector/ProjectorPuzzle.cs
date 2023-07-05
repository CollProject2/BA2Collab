using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorPuzzle : MonoBehaviour
{
    public GameObject interactParticle;
    public GameObject projectorLight;
    public ShelvesManager shelvesManager;
    public float interactRange;
    private bool isInteractable;
    

    public List<GameObject> imagesToShow;


    public string projectorSlideShowMemory;

    public void SetInteractable(bool state)
    {
        isInteractable = state;
    }

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
            // open HUD to give visual feedback
            interactParticle.SetActive(true);
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
                Collect();
        }
    }

    public void Collect()
    {
        //closes HUD when activating the puzzle 
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        projectorLight.SetActive(true);
        ShowPicture(0);
        Player.instance.animator.SetBool("isMoving", false);
        UIManager.instance.dialogues.StartDialogue(projectorSlideShowMemory);
        isInteractable = false;
    }

    public void ShowPicture(int picIndex)
    {
        foreach (var image in imagesToShow)
        {
            image.SetActive(false);
        }

        if (picIndex <= 2)
        {
            imagesToShow[picIndex].SetActive(true);
        }
        else if (picIndex == 3)
        {
            foreach (var image in imagesToShow)
            {
                image.SetActive(false);
            }
            projectorLight.SetActive(false);
            // call shelf
            shelvesManager.isInteractable = true;
        }
    }
}
