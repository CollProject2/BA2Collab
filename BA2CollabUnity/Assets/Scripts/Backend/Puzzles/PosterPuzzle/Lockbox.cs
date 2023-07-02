using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockbox : MonoBehaviour
{
    public GameObject interactParticle;
    public float interactRange;
    public bool isInteractable;
    public string LockBoxMemory;


    [Header("object")]
    [SerializeField] private GameObject lockPuzzleObj;

    [SerializeField] private GameObject lockBoxPivot;
    [SerializeField] private GameObject projectorPictures;
    [Header("positions")]
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform activePos;
    [SerializeField] private Transform projectorPicturesInitPos;
    [SerializeField] private Transform projectorPictureActivePos;
    [Header("Duration")]
    [SerializeField] private float lockBoxMoveDuration;
    [SerializeField] private float lockBoxOpenDur;

    private void Update()
    {
        if (!isInteractable) return;

        Interact();
    }

    public void Interact()
    {
        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
                Collect();
        }
    }

    public void Collect()
    {
        //closes HUD when activating the puzzle 
        isInteractable = false;
        interactParticle.SetActive(false);
        lockPuzzleObj.transform.DOMove(activePos.position, lockBoxMoveDuration);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
    }

    //for end of lock puzzle
    public void OpenLockBox()
    {
        //solve puzzle 
        lockPuzzleObj.transform.DOScale(new Vector3(8.7f, 8.7f, 8.7f), lockBoxOpenDur);
        lockPuzzleObj.transform.DORotate(new Vector3(-12, 0, 0), lockBoxMoveDuration).OnComplete(() =>
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

    //for end of fungus 
    public void MoveLockBoxAway()
    {

        projectorPictures.transform.DOMove(projectorPicturesInitPos.position, lockBoxMoveDuration).OnComplete(() =>
        {
            lockBoxPivot.transform.DOLocalRotate(new Vector3(0, 0, 0), lockBoxMoveDuration);
            lockPuzzleObj.transform.DOMove(initPos.position, lockBoxMoveDuration);
            lockPuzzleObj.transform.DOScale(new Vector3(1, 1, 1), lockBoxOpenDur);
            lockPuzzleObj.transform.DORotate(new Vector3(0, 0, 0), lockBoxMoveDuration);
        });

    }
}
