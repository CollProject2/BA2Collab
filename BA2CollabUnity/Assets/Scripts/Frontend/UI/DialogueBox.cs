using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DialogueBox : MonoBehaviour
{
    [Header("Objects")] 
    [SerializeField] private GameObject dialogueBoxParent;
    [SerializeField] private GameObject dialogueBoxTurningPart;
    [Header("Positions")] 
    [SerializeField] private Transform dialogueBoxActivePos;
    [SerializeField] private Transform dialoguePlayingTextPos;
    [SerializeField] private Transform dialogueBoxPassivePos;

    [Header("Durations")]
    [SerializeField] private float dialogueBoxMoveDur;
    [SerializeField] private float dialogueBoxTurnDur;


    [Header("Scale")]
    [Header("Curves")]
    [SerializeField] private AnimationCurve dialogueBoxMoveCurve;
    
    [Header("Items to Show")]
    

    public bool dialogueIsPlaying;
    

    public void MoveToActivePos()
    {
        dialogueBoxParent.transform.DOMove(dialogueBoxActivePos.position, dialogueBoxMoveDur)
            .SetEase(dialogueBoxMoveCurve);
    }

    public void MoveToPlayingTextPos()
    {
        dialogueBoxParent.transform.DOMove(dialoguePlayingTextPos.position, dialogueBoxMoveDur)
            .SetEase(dialogueBoxMoveCurve);
        dialogueBoxTurningPart.transform.DORotate(new Vector3(-110,0,0),dialogueBoxTurnDur,RotateMode.LocalAxisAdd);
    }

    public void OnDialogueEnd()
    {
        dialogueBoxParent.transform.DOMove(dialogueBoxActivePos.position, dialogueBoxMoveDur)
            .SetEase(dialogueBoxMoveCurve);
        dialogueBoxTurningPart.transform.DORotate(new Vector3(110,0,0),dialogueBoxTurnDur,RotateMode.LocalAxisAdd);
    }
}
