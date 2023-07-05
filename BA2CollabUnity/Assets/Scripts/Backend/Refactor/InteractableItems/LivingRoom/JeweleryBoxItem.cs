using DG.Tweening;
using UnityEngine;

public class JeweleryBoxItem : InteractableItem
{
    public string jeweleryMemory;

    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;

    [Header("object")]
    [SerializeField] private GameObject JeweleryPuzzleObj;
    [SerializeField] private GameObject jeweleryBoxPivot;
    [SerializeField] private GameObject Ring;

    [Header("positions")]
    [SerializeField] private Transform ringInitPos;
    [SerializeField] private Transform ringActivePos;

    [Header("Duration")]
    [SerializeField] private float jeweleryMoveDuration;
    [SerializeField] private float jeweleryBoxOpenDur;

    public override void Collect()
    {
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        SetIsComplete(true);
    }

    public void MoveItemAway()
    {
        Ring.transform.DOMove(ringInitPos.position, itemMovementDuration).OnComplete(() =>
        {
            jeweleryBoxPivot.transform.DOLocalRotate(new Vector3(0, 180, jeweleryBoxPivot.transform.rotation.z), jeweleryBoxOpenDur);
            JeweleryPuzzleObj.transform.DOMove(initPos.position, itemMovementDuration);
            JeweleryPuzzleObj.transform.DOScale(new Vector3(1, 1, 1), jeweleryBoxOpenDur);
        });
    }

    public void InstantiateAndMove()
    {
        JeweleryPuzzleObj.transform.DOMove(activePos.position, itemMovementDuration);
        JeweleryPuzzleObj.transform.DOScale(new Vector3(5f, 5f, 5f), jeweleryBoxOpenDur).OnComplete(() =>
        {
            jeweleryBoxPivot.transform.DOLocalRotate(new Vector3(80, 180, jeweleryBoxPivot.transform.rotation.z), jeweleryBoxOpenDur).OnComplete(() =>
            {
                Ring.transform.DOMove(ringActivePos.position, itemMovementDuration / 2).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    UIManager.instance.dialogues.StartDialogue(jeweleryMemory);
                });
            });
        });
    }
}
