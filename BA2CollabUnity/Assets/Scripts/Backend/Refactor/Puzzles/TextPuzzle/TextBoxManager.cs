using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxManager : MonoBehaviour
{
    public static TextBoxManager instance = null;
    public string associatedMemory;
    public bool isInteractable;
    public List<TextBlock> isRight = new();
    public TextBlock currentTextBlock;
    private bool isComplete;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        ResetOnAwake();

    }

    //set this as the current block
    public void SetCurrentTextBlock(TextBlock textBlock)
    {
        currentTextBlock = textBlock;
    }

    public void CallCheck()
    {
        if (WordsAreCorrect())
        {
            UIManager.instance.dialogues.StartDialogue(associatedMemory);
            isInteractable = false;
            isComplete = true;
            StoryManager.instance.AdvanceGameState();
        }
    }
    public bool IsComplete()
    {
        return isComplete;
    }
    private bool WordsAreCorrect()
    {
        foreach (TextBlock v in isRight)
            if (!v.solved) return false;
        return true;
    }

    internal void MoveText(GameObject gameObject, Transform transform)
    {
        gameObject.transform.position = transform.position;
    }

    public void ResetOnAwake()
    {
        foreach (TextBlock v in isRight)
        {
            v.solved = false;
        }
    }

    internal void Activate()
    {
        isInteractable = true;
    }
}
