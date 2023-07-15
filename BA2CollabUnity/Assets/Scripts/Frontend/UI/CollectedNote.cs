using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fungus;
using UnityEngine;
using UnityEngine.Events;

public class CollectedNote : MonoBehaviour
{
    private Vector3 defaultPos;
    private bool isOpen;
    
    private void Start()
    {
        defaultPos = transform.localPosition;
        isOpen = false;
        gameObject.SetActive(false);
    }
    // input
    private void OnMouseDown()
    {
        //only when the journal is open
        if(!UIManager.instance.MainMenuUI.journalIsOpen) return;
        // only one note is open at a given time
        if (JournalMenuUI.instance.aNoteIsOpen == false && !isOpen)
        {
            OpenNote();
        }
        else if (JournalMenuUI.instance.aNoteIsOpen  && isOpen)
        {
            CloseNote();
        }
    }
    
    public void OpenNote()
    {
        // opening animation
        isOpen = true;
        JournalMenuUI.instance.aNoteIsOpen = true;
        transform.DOMove(JournalMenuUI.instance.activeNotePos.position, JournalMenuUI.instance.enlargeTime);
        transform.DOScale(new Vector3(200,200 ,200 ),JournalMenuUI.instance.enlargeTime);
    }

    public void CloseNote()
    {
        // closing Animation
        transform.DOLocalMove(defaultPos, JournalMenuUI.instance.enlargeTime);
        transform.DOScale(new Vector3(75, 75, 75),JournalMenuUI.instance.enlargeTime).OnComplete(() =>
        {
            isOpen = false;
            JournalMenuUI.instance.aNoteIsOpen = false;
        });
    }
}
