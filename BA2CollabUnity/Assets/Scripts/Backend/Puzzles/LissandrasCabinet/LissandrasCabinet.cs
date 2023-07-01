using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LissandrasCabinet : MonoBehaviour
{
    public GameObject interactParticle;
    public GameObject CubeObject;
    public float interactRange;
    public bool isInteractable;

    [Header("object")] 
    [SerializeField] private GameObject cabinetDoor;
    [Header("positions")]
    [SerializeField] private Transform cubeActivePos;
    [SerializeField] private Transform cabinetDoorActivePos;
    [Header("Duration")]
    [SerializeField] private float cabinetDoorMovementDuration;
    
    private void Update()
    {
        if (isInteractable && isActiveAndEnabled)
        {
            InstantiateAndMove();
            isInteractable = false;
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
        cabinetDoor.SetActive(true);
        cabinetDoor.transform.DOMove(cabinetDoorActivePos.position, 1).OnComplete(() =>
        {
            CubeObject.transform.DOMove(cubeActivePos.position, 1).OnComplete(() =>
            {
                CubeObject.GetComponent<BlockCollect>().SetInteractable(true);
                Destroy(this);
            });
        });
    }
}
