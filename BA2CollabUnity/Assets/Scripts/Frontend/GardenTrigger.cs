using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        CameraManager.instance.CameraZoomInGarden();
        gameObject.GetComponent<Collider>().enabled = false;
        Player.instance.inGarden = true;
    }
}
