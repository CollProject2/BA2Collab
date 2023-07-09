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

    private void OnMouseDown()
    {
        if(!UIManager.instance.MainMenuUI.journalIsOpen) return;
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
        isOpen = true;
        JournalMenuUI.instance.aNoteIsOpen = true;
        transform.DOMove(JournalMenuUI.instance.activeNotePos.position, JournalMenuUI.instance.enlargeTime);
        transform.DOScale(new Vector3(200,200 ,200 ),JournalMenuUI.instance.enlargeTime);
    }

    public void CloseNote()
    {
        transform.DOLocalMove(defaultPos, JournalMenuUI.instance.enlargeTime);
        transform.DOScale(new Vector3(75, 75, 75),JournalMenuUI.instance.enlargeTime).OnComplete(() =>
        {
            isOpen = false;
            JournalMenuUI.instance.aNoteIsOpen = false;
        });
    }
}
