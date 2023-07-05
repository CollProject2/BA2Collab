using DG.Tweening;
using UnityEngine;

public class MovingBoxItem : InteractableItem
{
    public enum MovingBoxState
    {
        DroppingPuck,
        PickingUpBox,
        DroppingBox,
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

    protected override void Update()
    {
        base.Update();
        UpdateBoxState();
    }

    protected override void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            switch (boxState)
            {
                case MovingBoxState.DroppingPuck:
                    Player.instance.hasBear = false;
                    UIManager.instance.dialogues.StartDialogue(bearDropMemory);
                    Collect();
                    break;
                case MovingBoxState.PickingUpBox:
                    UIManager.instance.dialogues.StartDialogue(PickUpBoxAndBearMemory);
                    PickUpBox();
                    break;
                case MovingBoxState.DroppingBox:
                    UIManager.instance.dialogues.StartDialogue(movingBoxDropMemory);
                    PutDownBox();
                    break;
                case MovingBoxState.End:
                    UIManager.instance.dialogues.StartDialogue(endingMemory);
                    break;
            }
            SetIsComplete(true);
        }
    }

    public override void Collect()
    {
        ChangeValues();
        var bear = Instantiate(bearModel, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
        bear.transform.DOMove(transform.position, 1).OnComplete(() =>
        {
            bear.transform.parent = transform;
        });
    }

    private void PickUpBox()
    {
        ChangeValues();
        transform.DOMove(Player.instance.playerCarrypos.position, 1).OnComplete(
            () =>
            {
                transform.parent = Player.instance.transform;
                Player.instance.SetCanMove(true);
            });

    }

    public void PutDownBox()
    {
        if (Player.instance.CheckDistanceWithPlayer(boxPos.position) < interactRange && !Player.instance.isSolving)
        {
            ChangeValues();
            gameObject.transform.DOMove(boxPos.position, 1).OnComplete(() =>
            {
                gameObject.transform.parent = boxPos;
            });
        }
       
    }

    private void UpdateBoxState()
    {
        // Check the conditions for transitioning to the next state
        switch (boxState)
        {
            case MovingBoxState.DroppingPuck:
                if (Player.instance.hasBear)
                    boxState = MovingBoxState.PickingUpBox;
                break;
            case MovingBoxState.PickingUpBox:
                if (!Player.instance.hasMovingBox)
                    boxState = MovingBoxState.DroppingBox;
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
                Player.instance.SetCanMove(false);
                break;
            case MovingBoxState.DroppingBox:
                Player.instance.hasMovingBox = false;
                interactParticle.SetActive(false);
                break;
            case MovingBoxState.End:
                interactParticle.SetActive(false);
                break;
        }


    }

}
