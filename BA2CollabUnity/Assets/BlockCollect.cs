using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            BlockManager.instance.missingBlocks--;
            BlockManager.instance.DisplayNextBlock();
            //do other things yay u collected a block
            Destroy(gameObject);
        }
    }
}
