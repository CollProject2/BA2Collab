using UnityEngine;

public class Poster : MonoBehaviour
{
    public string posterMemory;
    public float interactRange;
    public bool isInteractable;
    
    public void Update()
    {
        if (!isInteractable) return;

        Interact(); 
       
    }
    public void SetInteractable(bool state)
    {
        isInteractable = state;
    }

    void Interact()
    {
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            // open HUD to give visual feedback

            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.instance.RecallMemory(posterMemory);
                Player.instance.hasPoster = true;
                Player.instance.SetCanMove(false);
                Player.instance.animator.SetBool("isMoving", false);
                SetInteractable(false);
                ItemUIManager.Instance.ToggleItem(0);
                Destroy(gameObject);
            }
        }
    }


}
