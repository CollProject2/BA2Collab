using UnityEngine;

public class BearZone : MonoBehaviour
{
    public PlayerMemory bearMemory;
    public int interactRange;
    bool isPlaced;
    public Animator boxAnimator;
    public GameObject interactParticle;

    private void Awake()
    {
        isPlaced = false;
    }
    public void Update()
    {
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !isPlaced && Player.instance.hasBear)
        {
            // open HUD to give visual feedback

            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                // put bear in box
                boxAnimator.SetTrigger("PutBearInBox");
                ChangeValues();
                
            }
        }
        else
        {
            // close HUD
        }
    }

    private void ChangeValues()
    {
        isPlaced = true;
        Player.instance.hasBear = false;
        Player.instance.RecallMemory(bearMemory);
    }
}
