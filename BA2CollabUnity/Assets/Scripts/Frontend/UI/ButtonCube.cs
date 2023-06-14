using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonCube : MonoBehaviour
{
    public UnityEvent OnPressed = new UnityEvent();

    private void OnMouseEnter()
    {
        // lights
    }

    private void OnMouseDown()
    {
        OnPressed.Invoke();
    }
}
