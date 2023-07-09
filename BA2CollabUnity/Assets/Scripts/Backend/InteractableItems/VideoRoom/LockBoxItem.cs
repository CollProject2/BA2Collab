using DG.Tweening;
using UnityEngine;

public class LockBoxItem : InteractableItem
{
    public string LockBoxMemory;
    [Header("object")]
    
    [SerializeField] private GameObject lockPuzzleObj;

    [SerializeField] private GameObject tinyLock;
    
    [Header("positions")]
    [SerializeField] private Transform puzzleInitPos;
    [SerializeField] private Transform puzzleIActivePos;
    [Header("Duration")]
    [SerializeField] private float lockBoxMoveDuration;

    public GameObject Waldo;
    


    protected override void Collect()
    {
        base.Collect();
        lockPuzzleObj.SetActive(true);
        lockPuzzleObj.transform.DOMove(puzzleIActivePos.position, lockBoxMoveDuration);
        SetIsComplete(true);
    }

    public void MoveItemAway()
    {
        tinyLock.SetActive(false);
    }

    public void OpenLockBox()
    {
        lockPuzzleObj.transform.DOMove(puzzleInitPos.position, lockBoxMoveDuration);
        UIManager.instance.dialogues.StartDialogue(LockBoxMemory);
    }

}
