using DG.Tweening;
using UnityEngine;

public class GlassesItem : InteractableItem
{
    public Glasses glasses;
    public Lockbox lockBox;
    [SerializeField] private GameObject glassesModel;

    protected override void Interact()
    {
        base.Interact();
    }

    public override void Collect()
    {
        interactParticle.SetActive(false);
        Player.instance.SetCanMove(false);
        Player.instance.animator.SetBool("isMoving", false);
        ActivateGlassesOnCamera();
        isInteractable = false;
    }

    public override void MoveItemAway()
    {
        // move glasses away 
    }

    public void ActivateGlassesOnCamera()
    {
        glassesModel.SetActive(true);
        Player.instance.hasGlasses = true;
        lockBox.isInteractable = true;
        glassesModel.transform.DOLocalMove(new Vector3(0, -1, 3), 1).OnComplete(() =>
        {
            glassesModel.transform.DOLocalMove(new Vector3(0, -1, 0.16f), 1).OnComplete(() =>
            {
                glassesModel.transform.DOLocalMove(new Vector3(0, -1, -1), 1);
                glasses.gameObject.SetActive(true);
                glasses.InitializeGlasses();
            });
        });
    }
}


