using UnityEngine;

public class EntranceTriggerLantern : MonoBehaviour
{
    public PlayerMemory triggerMemory;
    public float interactRange;
    private bool isInteractable;

    public void Update()
    {
        if (isInteractable)
            Interact();
    }

    void Interact()
    {
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
                ChangeValues();
        }
    }

    private void ChangeValues()
    {
        Player.instance.RecallMemory(triggerMemory);
        isInteractable = false;
        //light to the door 
    }
}
