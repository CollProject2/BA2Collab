using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector2d : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PP"))
        {
            Puzzle2DManager.instance.isRight[other.gameObject.GetComponent<PicturePiece>().id].solved = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PP"))
        {
            Puzzle2DManager.instance.isRight[other.gameObject.GetComponent<PicturePiece>().id].solved = false;
        }
    }
}
