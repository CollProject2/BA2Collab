using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [Range(0,5)]
    public int noteId;

    private void Update()
    {
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < 1.5f && !Player.instance.isSolving)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                JournalMenuUI.instance.CollectNote(noteId);
                Destroy(gameObject);
            }
        }
    }
    
    
}
