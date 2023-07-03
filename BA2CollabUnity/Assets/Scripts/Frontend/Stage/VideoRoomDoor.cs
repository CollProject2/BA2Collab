using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class VideoRoomDoor : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] private GameObject middleDoor;
    

    [Header("Positions")]
    [SerializeField] private Transform middleDoorEndPos;
    

    [Header("Durations")]
    [SerializeField] private float doorMoveDuration = 2;
    
    private void OnTriggerEnter(Collider other)
    {
        MoveObjects();
    }

    private void MoveObjects()
    {
        middleDoor.transform.DOMove(middleDoorEndPos.position, doorMoveDuration);
    }
}
