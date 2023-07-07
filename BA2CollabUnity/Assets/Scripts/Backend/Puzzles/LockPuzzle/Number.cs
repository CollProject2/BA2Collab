using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Number : MonoBehaviour
{
    private static Dictionary<RotationLR, Vector3> rotationDirections = new()
    {
        {RotationLR.Left, new Vector3(0, 0, -35.8f)},
        {RotationLR.Right, new Vector3(0, 0, 35.8f)},
    };

    public int id;
    public bool solved;
    private bool isRotating;
    public bool isInteractable;

    private void Awake()
    {
        solved = false;
        isRotating = false;
        //isInteractable = false;
    }
    private void OnMouseDown()
    {
        //prevent clicking before the puzzle is loaded
        if (!isInteractable) return;

        //set the new Letter as the current letter
        LockManager.instance.SetCurrentNumber(this);
    }
    public void RotateNumber(RotationLR direction)
    {
        //if its rotating we don't rotate
        if (isRotating) return;
        //we set rotating to true
        isRotating = true;
        //rotate and will call the wincon check and turn rotating false after the rotation is complete
        transform.DORotate(rotationDirections[direction], 0.3f, RotateMode.LocalAxisAdd).OnComplete(IsDone);
    }
    private void IsDone()
    {
        isRotating = false;
        LockManager.instance.CallCheck();
    }
}