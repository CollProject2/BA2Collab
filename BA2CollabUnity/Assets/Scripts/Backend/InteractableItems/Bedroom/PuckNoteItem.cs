using DG.Tweening;
using UnityEngine;

public class PuckNoteItem : InteractableItem
{
    protected override void Collect()
    {
        base.Collect();
        SetIsComplete(true);
        Player.instance.SetCanMove(true);
        //itemObject.transform.DOMove(activePos.position, itemMovementDuration).OnComplete(()=> canExitNote = true);
    }
}