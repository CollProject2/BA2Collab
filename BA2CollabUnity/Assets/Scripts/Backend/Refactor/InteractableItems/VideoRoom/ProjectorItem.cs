using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorItem : InteractableItem
{
    public GameObject projectorLight;
    public ShelvesManager shelvesManager;
    public List<GameObject> imagesToShow;

    public string projectorSlideShowMemory;

    protected override void Interact()
    {
        base.Interact();
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
                Collect();
        }
    }

    public override void Collect()
    {
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        projectorLight.SetActive(true);
        ShowPicture(0);
        Player.instance.animator.SetBool("isMoving", false);
        UIManager.instance.dialogues.StartDialogue(projectorSlideShowMemory);
        isInteractable = false;
    }

    public override void MoveItemAway()
    {
        //it stays lol
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
