using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoRoomDoor : MonoBehaviour
{
    public float interactRange;
    private void Update()
    {
        InteractWithDoor();
    }
    
    private void InteractWithDoor()
    {
        // add here a condition to check if the video room projector puzzle is solved
        
        //if the player is in range of the item, not solving a puzzle and the puzzle is not hidden
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
               Environment.instance.stageMovingParts.OnVideoRoomDoorInteract();
            }
        }
    }
}
