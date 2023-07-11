using Highlighters;
using UnityEngine;

public abstract class InteractableItem : MonoBehaviour
{
    public Highlighter interactParticle;
    protected float interactRange;
    protected bool isInteractable;
    protected bool isComplete;
    protected bool isCollected;
    protected float itemMovementDuration;
    protected bool hasToMove;

    protected virtual void Awake()
    {
        interactRange = 1.5f;
        isInteractable = false;
        isComplete = false;
        itemMovementDuration = 2.83f;
        isCollected = false;
    }

    public virtual void SetInteractable(bool state)
    {
        isInteractable = state;
    }

    public virtual void SetIsComplete(bool state)
    {
        isInteractable = !state;
        isComplete = state;
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
        if (!isCollected)
        {
            interactParticle.enabled = true;
            if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.interact,Vector3.zero);
                    Collect();
                }
            }
        }
    }
    protected virtual void Collect()
    {
        interactParticle.enabled = false;
        Player.instance.SetCanMove(false);

        if (hasToMove)
            InstantiateAndMove();
        isCollected = true;
    }

    public virtual void InstantiateAndMove() { }

}
