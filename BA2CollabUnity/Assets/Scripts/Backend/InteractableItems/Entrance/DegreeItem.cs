using DG.Tweening;
using UnityEngine;

public class DegreeItem : InteractableItem
{
    [Header("object")]
    [SerializeField] private GameObject textPuzzle;

    [Header("Duration")]
    [SerializeField] private float textPuzzleMovementDuration;
    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    protected override void Awake()
    {
        base.Awake();
        hasToMove = true;
    }

    public override void InstantiateAndMove()
    {
        textPuzzle.SetActive(true);
        textPuzzle.transform.DOMove(activePos.position, textPuzzleMovementDuration);
        SetIsComplete(true);
    }

    public void MoveItemAway()
    {
        textPuzzle.transform.DOMove(initPos.position, textPuzzleMovementDuration).OnComplete(() =>
        {

            textPuzzle.SetActive(false);
            TextBoxManager.instance.ResetOnAwake();
        });
    }
}
