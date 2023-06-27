using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBoxManager : MonoBehaviour
{
    public static TextBoxManager instance = null;
    public PlayerMemory associatedMemory;
    public bool isActive;
    public List<TextBlock> isRight = new();
    public TextBlock currentTextBlock;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        // isActive = false;

    }
    private void Update()
    {
        Player.instance.SetCanMove(false);
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
            isActive = false;
            //recall memory
            Player.instance.RecallMemory(associatedMemory);
            Destroy(this);
        }
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
}
