using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorItem : InteractableItem
{
    public GameObject projectorLight;
    public List<GameObject> imagesToShow;

    public string projectorSlideShowMemory;


    public override void Collect()
    {
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        projectorLight.SetActive(true);
        ShowPicture(0);
        Player.instance.animator.SetBool("isMoving", false);
        UIManager.instance.dialogues.StartDialogue(projectorSlideShowMemory);
        SetIsComplete(true);
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
        }
    }

}
