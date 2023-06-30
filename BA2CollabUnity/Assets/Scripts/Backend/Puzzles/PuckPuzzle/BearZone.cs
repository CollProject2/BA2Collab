using DG.Tweening;
using UnityEngine;

public class BearZone : MonoBehaviour
{
    public PlayerMemory bearMemory;
    public float interactRange;
    bool isPlaced;
    public Animator boxAnimator;
    public GameObject interactParticle;
    private bool isInteractable;
    public GameObject bearModel;
    public Item puzzleItemOffice3d;
    

    private void Awake()
    {
        isPlaced = false;
    }
    public void Update()
    {
        if (isInteractable)
        {
            Interact();;
        }
    }

    void Interact()
    {
        
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !isPlaced && Player.instance.hasBear)
        {
            // open HUD to give visual feedback

            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                // put bear in box
                // animation trigger 
                boxAnimator.SetTrigger("PutBearInBox");
                ChangeValues();
                isInteractable = false;
                Player.instance.animator.SetBool("isMoving", false);
                ItemUIManager.Instance.ToggleItem(2);
                var bear = Instantiate(bearModel,
                    new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
                    transform.rotation);
                bear.transform.DOMove(transform.position, 1).OnComplete(()=>puzzleItemOffice3d.SetIsHidden(false) );
            }
        }
        
    }

    public void SetBoxActive()
    {
        isInteractable = true;
    }

    private void ChangeValues()
    {
        isPlaced = true;
        Player.instance.hasBear = false;
        Player.instance.RecallMemory(bearMemory);
    }
}
