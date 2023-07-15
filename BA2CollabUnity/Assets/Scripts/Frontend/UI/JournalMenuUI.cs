using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalMenuUI : MonoBehaviour
{
    // works as a journal manager, keeps track of notes in the game and opens them as they are collected.
    public static JournalMenuUI instance { get; private set; }

    [Header("positions")]
    public Transform activeNotePos;
    [Header("duration")] 
    public float enlargeTime;

    [Header("notes")] 
    public List<GameObject> notes;


    public bool aNoteIsOpen;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("MORE THAN ONE JournalMenuUI !!!");
        }
        instance = this;
        
    }

    public void CollectNote(int noteId)
    {
        notes[noteId].SetActive(true);
    }
}
