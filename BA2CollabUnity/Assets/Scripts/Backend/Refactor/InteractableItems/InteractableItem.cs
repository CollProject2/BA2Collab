using DG.Tweening;
using UnityEngine;

public abstract class InteractableItem : MonoBehaviour
{
    public GameObject interactParticle;
    protected float interactRange;
    protected bool isInteractable;
    protected bool isComplete;

    [Header("object")]
    [SerializeField] protected GameObject itemObject;
    [Header("positions")]
    [SerializeField] protected Transform initPos;
    [SerializeField] protected Transform activePos;
    [Header("Duration")]
    [SerializeField] protected float itemMovementDuration;

    protected virtual void Awake()
    {
        interactRange = 1.5f;
        itemObject.SetActive(false);
        isInteractable = false;
        isComplete = false;
    }

    public virtual void SetInteractable(bool state)
    {
        isInteractable = state;
    }

    public virtual void SetIsComplete(bool state)
    {
        isInteractable = state;
    }

    public virtual bool IsComplete()
    {
        return isComplete;
    }

    protected virtual void Update()
    {
        if (!isInteractable) return;
        Interact();
    }

    protected virtual void Interact()
    {
        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            if (Input.GetKeyDown(KeyCode.E))
                Collect();
        }
    }
    
    protected virtual void InstantiateAndMove()
    {
        itemObject.SetActive(true);
        itemObject.transform.DOMove(activePos.position, itemMovementDuration);
    }

    public abstract void Collect();

    public abstract void MoveItemAway();
}
