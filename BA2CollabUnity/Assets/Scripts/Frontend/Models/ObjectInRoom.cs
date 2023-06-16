using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInRoom : MonoBehaviour
{
    public enum ObjectType
    {
        Wall,
        Furniture,
        Static
    }

    public ObjectType objectType;
    public Vector3 startPos;

    private void Awake()
    {
        startPos = transform.localPosition;
    }
    
}
