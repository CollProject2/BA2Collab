using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPosterPuzzle : MonoBehaviour
{
    public PlayerMemory posterMemory2;
    public int interactRange;

    public void Update()
    {
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving && Player.instance.hasPoster)
        {
            // open HUD to give visual feedback

            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                GetComponent<MeshRenderer>().enabled = true;
                Player.instance.hasGlasses = true;
                Player.instance.RecallMemory(posterMemory2);
            }
        }
        else
        {
            // close HUD
        }
    }
}
