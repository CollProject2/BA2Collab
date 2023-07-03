using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FirePlaceSecretDrawer : MonoBehaviour
{
    public GameObject CubeObject;
    public bool isInteractable;

    [Header("object")] 
    [SerializeField] private GameObject secretCabinetDoor;
    
    
    private void Update()
    {
        if (isInteractable)
        {
            InstantiateAndMove();
            isInteractable = false;
        }
    }

    void InstantiateAndMove()
    {
        secretCabinetDoor.transform.DOLocalRotate(new Vector3(-50,-121.215f,0), 1).OnComplete(() =>
        {
            CubeObject.GetComponent<BlockCollect>().SetInteractable(true);
            Destroy(this);
        });
    }
}
