using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PartialRoomLoader : MonoBehaviour
{
    public GameObject roomObjects;

    

    private void Start()
    {
        
        roomObjects.transform.position = new Vector3(roomObjects.transform.position.x, 10, roomObjects.transform.position.z);
    }

    public void PartialLoad()
    {
        //Debug.Log("partially load");
        roomObjects.transform.DOMove(Vector3.zero, Environment.instance.turnDuration);
    }

    public void PartiallyUnload()
    {
        //Debug.Log("partially Unload");
        roomObjects.transform.DOMove(new Vector3(roomObjects.transform.position.x, 10, roomObjects.transform.position.z), Environment.instance.turnDuration);
    }
    
    
    
    
}
