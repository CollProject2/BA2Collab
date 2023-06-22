using UnityEngine;

public class Poster : MonoBehaviour
{
    public PlayerMemory posterMemory;
    public int interactRange;
    
    public void Update()
    {
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            // open HUD to give visual feedback

            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                Player.instance.RecallMemory(posterMemory);
                Destroy(gameObject);
            }
        }
        else
        {
            // close HUD
        }
    }


}
