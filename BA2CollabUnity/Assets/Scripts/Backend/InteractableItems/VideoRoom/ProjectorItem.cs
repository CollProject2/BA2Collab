using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorItem : InteractableItem
{
    public GameObject projectorLight;
    public List<GameObject> imagesToShow;

    public string projectorSlideShowMemory;


    protected override void Collect()
    {
        base.Collect();
        projectorLight.SetActive(true);
        LightManager.instance.OpenMiddleLight(false);
        ShowPicture(0);
        Player.instance.isSolving = true;
        UIManager.instance.dialogues.StartDialogue(projectorSlideShowMemory);
    }

    public void ShowPicture(int picIndex)
    {
        foreach (var image in imagesToShow)
        {
            image.SetActive(false);
        }

        if (picIndex <= 1)
        {
            imagesToShow[picIndex].SetActive(true);
        }
        else if (picIndex == 2)
        {
            foreach (var image in imagesToShow)
            {
                image.SetActive(false);
            }
            projectorLight.SetActive(false);
            LightManager.instance.OpenMiddleLight(true);
            SetIsComplete(true);
        }
    }

}
