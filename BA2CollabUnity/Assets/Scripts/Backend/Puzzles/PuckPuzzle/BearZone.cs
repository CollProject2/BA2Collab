using UnityEngine;

public class BearZone : MonoBehaviour
{
    public PlayerMemory bearMemory;
    public GameObject bear;
    public int interactRange;
    bool isPlaced;

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
                Instantiate(bear);
                bear.transform.position = new Vector3(32, 0.5f, 3); //hardcoded for now
                ChangeValues();
                Destroy(gameObject);
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
