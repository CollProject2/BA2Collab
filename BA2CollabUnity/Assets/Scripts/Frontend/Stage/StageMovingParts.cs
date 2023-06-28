using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StageMovingParts : MonoBehaviour
{
    [Header("Objects")] 
    [SerializeField] private GameObject videoRoomDoor;
    [SerializeField] private GameObject videoRoomWall1;
    [SerializeField] private GameObject videoRoomWall2;

    // [Header("Positions")]
    //
    // [Header("Durations")]
    // [Header("Curves")]

    public void OnVideoRoomDoorInteract()
    {
        videoRoomDoor.transform.DOMove(
            new Vector3(videoRoomDoor.transform.position.x, videoRoomDoor.transform.position.y + 6,
                videoRoomDoor.transform.position.z), 2);
        videoRoomWall1.transform.DOMove(
            new Vector3(videoRoomWall1.transform.position.x - 6, videoRoomWall1.transform.position.y,
                videoRoomWall1.transform.position.z), 2);
        videoRoomWall2.transform.DOMove(
            new Vector3(videoRoomWall2.transform.position.x + 6, videoRoomWall2.transform.position.y,
                videoRoomWall2.transform.position.z), 2);
    }
    
}
