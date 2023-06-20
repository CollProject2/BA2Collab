using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    // https://www.youtube.com/watch?v=rcBHIOjZDpk 
    
    [field: Header("MemoryMusic")]
    [field: SerializeField] public EventReference memoryMusic_1 { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("!!! Error: MORE THAN ONE FMODEVENTS SCRIPTS");
        }

        instance = this;
    }
}
