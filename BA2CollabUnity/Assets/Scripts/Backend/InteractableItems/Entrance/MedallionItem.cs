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
    protected override void Awake()
    {
        base.Awake();
        hasToMove = true;
      
    }

    public override void InstantiateAndMove()
    {
        medallionPuzzleObj.SetActive(true);
        medallionPuzzleObj.transform.DOMove(activePos.position, medallionMovementDuration);
        medallionPuzzleObj.transform.DOScale(new Vector3(8.7f, 8.7f, 8.7f), medallionOpenDuration);
        medallionPuzzleObj.transform.DORotate(new Vector3(0, 0, -90), medallionMovementDuration).OnComplete(() =>
        {
            medallionPivot.transform.DOLocalRotate(new Vector3(0, -200, 0), medallionOpenDuration).OnComplete(() =>
            {
                letter.SetActive(true);
                UIManager.instance.dialogues.StartDialogue(medallionMemory);
            });
        });
    }

    public void MoveItemAway()
    {
        letter.SetActive(false);
        medallionPivot.transform.DOLocalRotate(new Vector3(0, -90, 0), medallionOpenDuration).OnComplete(() =>
        {
            medallionPuzzleObj.transform.DOMove(initPos.position, medallionMovementDuration);
            medallionPuzzleObj.transform.DOScale(new Vector3(0.6f, 0.6f, 0.6f), medallionOpenDuration);
            medallionPuzzleObj.transform.DORotate(new Vector3(90, 160, -315), medallionMovementDuration);
            SetIsComplete(true);
        });
    }
}
