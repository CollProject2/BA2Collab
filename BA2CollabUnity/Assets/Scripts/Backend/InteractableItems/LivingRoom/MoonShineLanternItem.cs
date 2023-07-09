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
    protected override void Awake()
    {
        base.Awake();
        hasToMove = true;
    }

    public override void InstantiateAndMove()
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
        itemObject.transform.DOScale(new Vector3(0.75f, 0.75f, 0.75f), itemMovementDuration);
        itemObject.transform.DOMove(initPos.position, itemMovementDuration).OnComplete(()=>
        {
            LightManager.instance.OpenLivingRoomEntranceDoorHighLight(true);
            SetIsComplete(true);
        });
    }


}
