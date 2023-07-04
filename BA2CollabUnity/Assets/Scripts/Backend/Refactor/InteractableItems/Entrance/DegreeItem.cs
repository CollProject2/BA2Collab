using DG.Tweening;
using UnityEngine;

public class DegreeItem : InteractableItem
{
    [Header("object")]
    [SerializeField] private GameObject textPuzzle;

    [Header("Duration")]
    [SerializeField] private float textPuzzleMovementDuration;

    public override void Collect()
    {
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        isInteractable = false;
    }

    protected override void InstantiateAndMove()
    {
        textPuzzle.SetActive(true);
        textPuzzle.transform.DOMove(activePos.position, textPuzzleMovementDuration);
    }

    public override void MoveItemAway()
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
