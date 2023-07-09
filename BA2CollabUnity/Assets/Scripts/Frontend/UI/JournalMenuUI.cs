using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalMenuUI : MonoBehaviour
{
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
