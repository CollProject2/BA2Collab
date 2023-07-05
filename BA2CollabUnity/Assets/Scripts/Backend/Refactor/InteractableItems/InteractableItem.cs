using DG.Tweening;
using UnityEngine;

public abstract class InteractableItem : MonoBehaviour
{
    public GameObject interactParticle;
    protected float interactRange;
    protected bool isInteractable;
    protected bool isComplete;
    protected float itemMovementDuration;

    protected virtual void Awake()
    {
        interactRange = 1.5f;
        isInteractable = false;
        isComplete = false;
        itemMovementDuration = 2.83f;
    }

    public virtual void SetInteractable(bool state)
    {
        isInteractable = state;
    }

    public virtual void SetIsComplete(bool state)
    {
        isInteractable = !state;
        isComplete = state;
        StoryManager.instance.AdvanceGameState();
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

    public abstract void Collect();

}
