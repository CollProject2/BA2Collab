using DG.Tweening;
using UnityEngine;

public class MovingBoxItem : InteractableItem
{
    public enum MovingBoxState
    {
        DroppingPuck,
        PickingUpBox,
        DroppingBox,
        BoxDropped,
        End
    }

    public MovingBoxState boxState;
    public Animator boxAnimator;
    public GameObject bearModel;
    public string bearDropMemory;
    public string PickUpBoxAndBearMemory;
    public string movingBoxDropMemory;
    public string endingMemory;
    public Transform boxPos;

    protected override void Awake()
    {
        base.Awake();
        Player.instance.hasBear = false;
        Player.instance.hasMovingBox = false;
    }

    protected override void Interact()
    {

        if (Player.instance.CheckDistanceWithPlayer(gameObject.transform.position) < interactRange && !Player.instance.isSolving)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch (boxState)
                {
                    case MovingBoxState.DroppingPuck:
                        Collect();
                        break;
                    case MovingBoxState.PickingUpBox:
                        PickUpBox();
                        break;
                    case MovingBoxState.DroppingBox:
                        PutDownBox();
                        break;
                    case MovingBoxState.End:
                        UIManager.instance.dialogues.StartDialogue(endingMemory);
                        break;
                }

            }
        }
    }

    protected override void Collect()
    {
        UIManager.instance.dialogues.StartDialogue(bearDropMemory);
        var bear = Instantiate(bearModel, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
        bear.transform.DOMove(transform.position, 1).OnComplete(() =>
        {
            bear.transform.parent = transform;
            ChangeValues();
        });
    }

    private void PickUpBox()
    {

        Player.instance.SetCanMove(false);
        transform.DOMove(Player.instance.playerCarrypos.position, 0.6f).OnComplete(
            () =>
            {
                transform.parent = Player.instance.transform;
                gameObject.transform.DOLocalRotate(new Vector3(0, -90, 0), 0.2f);
                UIManager.instance.dialogues.StartDialogue(PickUpBoxAndBearMemory);
                ChangeValues();
            });
    }

    public void PutDownBox()
    {
        
        if (Player.instance.CheckDistanceWithPlayer(boxPos.position) < interactRange && !Player.instance.isSolving)
        {
            Player.instance.SetCanMove(false);
            gameObject.transform.DOMove(boxPos.position, 1).OnComplete(() =>
            {
                gameObject.transform.parent = boxPos;
                UIManager.instance.dialogues.StartDialogue(movingBoxDropMemory);
                ChangeValues();
            });
        }

    }

    private void UpdateBoxState()
    {
        // Check the conditions for transitioning to the next state
        switch (boxState)
        {
            case MovingBoxState.DroppingPuck:
                boxState = MovingBoxState.PickingUpBox;
                break;
            case MovingBoxState.PickingUpBox:
                boxState = MovingBoxState.DroppingBox;
                break;
            case MovingBoxState.DroppingBox:
                boxState = MovingBoxState.BoxDropped;
                break;
        }
    }

    private void ChangeValues()
    {
        switch (boxState)
        {
            case MovingBoxState.DroppingPuck:
                boxAnimator.SetTrigger("PutBearInBox");
                Player.instance.hasBear = false;
                break;
            case MovingBoxState.PickingUpBox:
                Player.instance.hasMovingBox = true;
                interactParticle.SetActive(false);
                LightManager.instance.OpenOfficeMovingBoxHighLight(false);
                Player.instance.SetCanMove(true);
                break;
            case MovingBoxState.DroppingBox:
                Player.instance.hasMovingBox = false;
                interactParticle.SetActive(false);
                Player.instance.SetCanMove(true);
                break;
            case MovingBoxState.End:
                interactParticle.SetActive(false);
                break;
        }
        SetIsComplete(true);
        UpdateBoxState();
    }

}
