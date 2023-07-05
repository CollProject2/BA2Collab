using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public LockBoxItem lockbox;
    public bool isComplete;
    public bool isInteractable;
    public List<Number> isRight = new();
    public static LockManager instance = null;
    public Number currentRow;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        if (currentRow != null && isInteractable) { Player.instance.SetCanMove(false); CheckInput(); }
    }

    //set this as the current block
    public void SetCurrentNumber(Number nRow)
    {
        currentRow = nRow;
    }
    public void CallCheck()
    {
        if (RowsAreCorrect())
        {
            lockbox.OpenLockBox();
            isInteractable = false;
            isComplete = true;
            StoryManager.instance.AdvanceGameState();
        }

    }
    public bool IsComplete()
    {
        return isComplete;
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
            RotateNumberAt(currentRow.id, RotationLR.Left);
        else if (Input.GetKeyDown(KeyCode.D))
            RotateNumberAt(currentRow.id, RotationLR.Right);
    }

    //rotate the block with the given index in the list
    public void RotateNumberAt(int index, RotationLR direction)
    {
        if (index >= 0 && index < isRight.Count)
            isRight[index].RotateNumber(direction);
    }

    private bool RowsAreCorrect()
    {
        foreach (Number v in isRight)
            if (!v.solved) return false;
        return true;
    }
    internal void Activate()
    {
        isInteractable = true;
    }
}