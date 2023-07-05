using DG.Tweening;
using UnityEngine;

public class MedallionItem : InteractableItem
{

    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    [SerializeField] private GameObject medallionPuzzleObj;
    [SerializeField] private GameObject medallionPivot;
    [SerializeField] private GameObject letter;
    [SerializeField] private float medallionMovementDuration;
    [SerializeField] private float medallionOpenDuration;
    public string medallionMemory;

    public override void Collect()
    {
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Puzzle2DManager.instance.isInteractable = true;
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        SetIsComplete(true);
    }

    public void InstantiateAndMove()
    {
        medallionPuzzleObj.SetActive(true);
        medallionPuzzleObj.transform.DOMove(activePos.position, medallionMovementDuration);
        medallionPuzzleObj.transform.DOScale(new Vector3(8.7f, 8.7f, 8.7f), medallionOpenDuration);
        medallionPuzzleObj.transform.DORotate(new Vector3(0, -90, 0), medallionMovementDuration).OnComplete(() =>
        {
            medallionPivot.transform.DORotate(new Vector3(0, -90, 96), medallionOpenDuration).OnComplete(() =>
            {
                letter.SetActive(true);
                UIManager.instance.dialogues.StartDialogue(medallionMemory);
            });
        });
    }

    public void MoveItemAway()
    {
        letter.SetActive(false);
        medallionPivot.transform.DORotate(new Vector3(0, -90, 1), medallionOpenDuration).OnComplete(() =>
        {
            medallionPuzzleObj.transform.DOMove(initPos.position, medallionMovementDuration);
            medallionPuzzleObj.transform.DOScale(new Vector3(1, 1, 1), medallionOpenDuration);
            medallionPuzzleObj.transform.DORotate(new Vector3(0, -60, -90), medallionMovementDuration);
        });
    }
}
