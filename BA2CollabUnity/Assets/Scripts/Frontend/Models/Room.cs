using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{
    public List<ObjectInRoom> objectsInRoom;
    public float targetY;

    private void Awake()
    {
        foreach (Transform child in transform)
        {
            objectsInRoom.Add(child.GetComponent<ObjectInRoom>());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MoveObjectsAway();
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            MoveObjectsToStartPos();
        }
    }

    void MoveObjectsAway()
    {
        foreach (var obj in objectsInRoom)
        {
            switch (obj.objectType)
            {
                case ObjectInRoom.ObjectType.Static:
                    Debug.Log(obj + " this obj is static");
                    break;
                case ObjectInRoom.ObjectType.Furniture:
                    MoveAwayAnim(obj);
                    break;
                case ObjectInRoom.ObjectType.Wall:
                    MoveAwayAnim(obj);
                    break;
            }
        }
    }
    void MoveObjectsToStartPos()
    {
        foreach (var obj in objectsInRoom)
        {
            switch (obj.objectType)
            {
                case ObjectInRoom.ObjectType.Static:
                    Debug.Log(obj + " this obj is static");
                    break;
                case ObjectInRoom.ObjectType.Furniture:
                    MoveToStartPosAnim(obj);
                    break;
                case ObjectInRoom.ObjectType.Wall:
                    MoveToStartPosAnim(obj);
                    break;
            }
        }
    }

    void MoveAwayAnim(ObjectInRoom obj)
    {
        obj.transform.DOLocalMove(new Vector3(obj.startPos.x, targetY, obj.startPos.z), Random.Range(1.7f, 4.5f));
    }

    void MoveToStartPosAnim(ObjectInRoom obj)
    {
        obj.transform.DOLocalMove(new Vector3(obj.startPos.x, obj.startPos.y, obj.startPos.z), Random.Range(1.7f, 4.5f));

    }
    
    
    
}
