using DG.Tweening;
using UnityEngine;

public class LockBoxItem : InteractableItem
{

    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    public string LockBoxMemory;
    [Header("object")]
    [SerializeField] private GameObject lockItemObj;
    [SerializeField] private GameObject lockPuzzleObj;
    [SerializeField] private GameObject lockBoxPivot;
    [SerializeField] private GameObject projectorPictures;
    [Header("positions")]
    [SerializeField] private Transform puzzleInitPos;
    [SerializeField] private Transform puzzleIActivePos;
    [SerializeField] private Transform projectorPicturesInitPos;
    [SerializeField] private Transform projectorPictureActivePos;
    [Header("Duration")]
    [SerializeField] private float lockBoxMoveDuration;
    [SerializeField] private float lockBoxOpenDur;


    public override void Collect()
    {
        interactParticle.SetActive(false);
        lockPuzzleObj.SetActive(true);
        lockPuzzleObj.transform.DOMove(puzzleIActivePos.position, lockBoxMoveDuration);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        SetIsComplete(true);
    }

    public void MoveItemAway()
    {
        projectorPictures.transform.DOMove(projectorPicturesInitPos.position, lockBoxMoveDuration).OnComplete(() =>
        {
            lockBoxPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), lockBoxMoveDuration);
            lockItemObj.transform.DOMove(initPos.position, lockBoxMoveDuration);
            lockItemObj.transform.DOScale(new Vector3(1, 1, 1), lockBoxOpenDur);
            lockItemObj.transform.DORotate(new Vector3(0, 0, 0), lockBoxMoveDuration);
        });
    }

    public void OpenLockBox()
    {
        lockPuzzleObj.transform.DOMove(puzzleInitPos.position, lockBoxMoveDuration / 2);
        //solve puzzle 
        lockItemObj.transform.DOMove(activePos.position, lockBoxMoveDuration);
        lockItemObj.transform.DOScale(new Vector3(5f, 5, 5), lockBoxOpenDur);
        lockItemObj.transform.DORotate(new Vector3(5, 180, 0), lockBoxMoveDuration).OnComplete(() =>
        {
            lockBoxPivot.transform.DOLocalRotate(new Vector3(0, 0, -96), lockBoxOpenDur).OnComplete(() =>
            {
                projectorPictures.transform.DOLocalRotate(new Vector3(0, 540, 0), lockBoxMoveDuration, RotateMode.FastBeyond360);
                projectorPictures.transform.DOMove(projectorPictureActivePos.position, lockBoxMoveDuration / 2).SetEase(Ease.InOutSine).OnComplete(() =>
                {
                    UIManager.instance.dialogues.StartDialogue(LockBoxMemory);

                });
            });
        });
    }

}
