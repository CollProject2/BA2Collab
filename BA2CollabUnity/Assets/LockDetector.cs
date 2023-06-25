using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Number"))
        {
            LockManager.instance.isRight[other.gameObject.GetComponentInParent<Number>().id].solved = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Number"))
        {
            LockManager.instance.isRight[other.gameObject.GetComponentInParent<Number>().id].solved = false;
        }
    }
}
