using DG.Tweening;
using UnityEngine;

public class TextFrameItem : MonoBehaviour
{
    public GameObject interactParticle;
    private float interactRange;
    private bool isInteractable;


    [Header("object")]
    [SerializeField] private GameObject textPuzzle;
    [Header("positions")]
    [SerializeField] private Transform initPos;
    [SerializeField] private Transform activePos;
    [Header("Duration")]
    [SerializeField] private float textPuzzleMovementDuration;
    private void Awake()
    {
        interactRange = 1.5f;
        textPuzzle.SetActive(false);
        isInteractable = true;

    }

    private void Update()
    {
        if (!isInteractable) return;

        Interact();
    }

    public void Interact()
    {

        interactParticle.SetActive(true);
        if (Player.instance.CheckDistanceWithPlayer(transform.position) < interactRange && !Player.instance.isSolving)
        {
            //press E to collect
            if (Input.GetKeyDown(KeyCode.E))
            {
                Collect();
            }
        }
    }

    public void Collect()
    {
        //closes HUD when activating the puzzle 
        interactParticle.SetActive(false);
        InstantiateAndMove();
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        isInteractable = false;
    }

    void InstantiateAndMove()
    {
        textPuzzle.SetActive(true);
        textPuzzle.transform.DOMove(activePos.position, textPuzzleMovementDuration);
    }

    public void MoveTextPuzzleAway()
    {
        textPuzzle.transform.DOMove(initPos.position, textPuzzleMovementDuration).OnComplete(() =>
        {
            textPuzzle.SetActive(false);
            TextBoxManager.instance.ResetOnAwake();
            Destroy(textPuzzle.gameObject);
            Destroy(this);
        });
    }
    
    
}
