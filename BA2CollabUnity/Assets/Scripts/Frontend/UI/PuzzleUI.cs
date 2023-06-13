using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    // Properties
    //public bool isDisplayed { get; private set; }
    public List<GameObject> puzzleImages;
    public float openingDuration;

    public void OnButtonClickBlock(int direction)
    {
        BlockManager.instance.RotateBlockAt(BlockManager.instance.currentBlock.index, (RotationDirection)direction);
    }

    public void DisplayUIPuzzle(int puzzleNum)
    {
        puzzleImages[puzzleNum].GetComponent<RectTransform>().localScale = Vector3.zero;
        puzzleImages[puzzleNum].SetActive(true);
        puzzleImages[puzzleNum].GetComponent<RectTransform>().DOScaleX(1, openingDuration);
        puzzleImages[puzzleNum].GetComponent<RectTransform>().DOScaleY(1, openingDuration);
    }

    public void HideUIPuzzle(int puzzleNum)
    {
        puzzleImages[puzzleNum].GetComponent<RectTransform>().DOScaleX(0, openingDuration);
        puzzleImages[puzzleNum].GetComponent<RectTransform>().DOScaleY(0, openingDuration);
        puzzleImages[puzzleNum].SetActive(true);
    }

}
