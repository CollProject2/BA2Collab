using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenTrigger : MonoBehaviour
{
    public GameObject gardenCollider;
    private void OnTriggerEnter(Collider other)
    {
        CameraManager.instance.CameraZoomInGarden();
        gameObject.GetComponent<Collider>().enabled = false;
        Player.instance.inGarden = true;
        gardenCollider.SetActive(true);
        CheckListManager.instance.AdvanceChecklist(); //visit the garden == after zoom in?
    }
}
