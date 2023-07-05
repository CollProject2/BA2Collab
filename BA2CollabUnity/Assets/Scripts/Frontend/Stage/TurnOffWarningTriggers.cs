using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffWarningTriggers : MonoBehaviour
{
    public List<Warning> Warnings;

    private void OnTriggerEnter(Collider other)
    {
        foreach (var warning in Warnings)
        {
            if(warning !=null)
            Destroy(warning.gameObject);
        }
        
        Destroy(this);
    }
}
