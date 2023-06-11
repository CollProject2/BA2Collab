using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine;

public class PuzzleUI : MonoBehaviour
{
    // Properties
    public bool isDisplayed { get; private set; }
    public List<GameObject> puzzleImages;
    public float openingDuration;

    public void DisplayUIPuzzle(int puzzleNum)
    {
        puzzleImages[puzzleNum].gameObject.GetComponent<RectTransform>().localScale = Vector3.zero;
        puzzleImages[puzzleNum].gameObject.SetActive(true);
        puzzleImages[puzzleNum].gameObject.GetComponent<RectTransform>().DOScaleX(1, openingDuration);
        puzzleImages[puzzleNum].gameObject.GetComponent<RectTransform>().DOScaleY(1, openingDuration);

    }

    public void HideUIPuzzle(int puzzleNum)
    {
        puzzleImages[puzzleNum].gameObject.GetComponent<RectTransform>().DOScaleX(0, openingDuration);
        puzzleImages[puzzleNum].gameObject.GetComponent<RectTransform>().DOScaleY(0, openingDuration);
        puzzleImages[puzzleNum].gameObject.SetActive(true);
    }


}
