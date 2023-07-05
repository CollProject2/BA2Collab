using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Letter"))
        {
            ShelvesManager.instance.isRight[other.gameObject.GetComponent<Letter>().id].solved = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Letter"))
        {
            ShelvesManager.instance.isRight[other.gameObject.GetComponent<Letter>().id].solved = false;
        }
    }
}
