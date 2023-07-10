using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Fungus;
using UnityEngine;
[CommandInfo("puzzle", "YoursEverLetterOpenClose", "moves puzzle out of screen")]
public class YoursEverLetter_BlockCollect3_livingRoom : Command
{
    public GameObject YoursEverLetter;

    public bool opening;

    public override void OnEnter()
    {
        if (opening)
        {
            OpenLetter();
        }
        else
        {
            CloseLetter();
        }
        Continue();    
    }

    void OpenLetter()
    {
        YoursEverLetter.transform.DOMove(UIManager.instance.puzzleUI.blockPuzzleActivePos.position, 2);
    }

    void CloseLetter()
    {
        YoursEverLetter.transform.DOMove(UIManager.instance.puzzleUI.blockPuzzleInstantiatePos.position, 2);
    }
}
