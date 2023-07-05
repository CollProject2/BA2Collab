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
    public override void Collect()
    {
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        SetIsComplete(true); 
    }

    public void InstantiateAndMove()
    {
        textPuzzle.SetActive(true);
        textPuzzle.transform.DOMove(activePos.position, textPuzzleMovementDuration);
    }

    public void MoveItemAway()
    {
        textPuzzle.transform.DOMove(initPos.position, textPuzzleMovementDuration).OnComplete(() =>
        {
            textPuzzle.SetActive(false);
            TextBoxManager.instance.ResetOnAwake();
            Destroy(textPuzzle);
            Destroy(this);
        });
    }
}
