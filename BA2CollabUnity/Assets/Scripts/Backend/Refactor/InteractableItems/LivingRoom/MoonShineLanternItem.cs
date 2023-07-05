using DG.Tweening;
using UnityEngine;

public class MoonShineLanternItem : InteractableItem
{
    [Header("object")]
    [SerializeField] protected GameObject itemObject;
    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;

    public string moonshineLanternMemory;
    public override void Collect()
    {
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        InstantiateAndMove();
        SetIsComplete(true);
        
    }

    
    
    void InstantiateAndMove()
    {
        itemObject.SetActive(true);
        itemObject.transform.DOScale(new Vector3(3.5f, 3.5f, 3.5f), itemMovementDuration);
        itemObject.transform.DOMove(activePos.position, itemMovementDuration).OnComplete(() =>
        {
            UIManager.instance.dialogues.StartDialogue(moonshineLanternMemory);
            
        });
    }

    public void MoveMoonshineLanternAway()
    {
        SetIsComplete(true);
        itemObject.transform.DOScale(new Vector3(1, 1, 1), itemMovementDuration);
        itemObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(()=>
        {
            LightManager.instance.OpenLivingRoomEntranceDoorHighLight(true);
            
        });
    }


}
