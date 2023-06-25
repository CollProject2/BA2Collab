using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    public PlayerMemory bearMemory;
    public int interactRange;
    public bool isInteractable;

    public void Update()
    {

        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving && isInteractable)
        {
            // open HUD to give visual feedback

            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.instance.RecallMemory(bearMemory);
                Player.instance.hasBear = true;
                Destroy(gameObject);
            }
        }
        else
        {
            // close HUD
        }
    }

}
