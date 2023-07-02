using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    public string associatedMemory;
    public bool isActive;
    public List<Number> isRight = new();
    public static LockManager instance = null;
    public Number currentRow;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        // isActive = false;

    }
    private void Update()
    {
        Player.instance.SetCanMove(false);
        if (currentRow != null && isActive) { CheckInput(); }
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
            isActive = false;
            //recall memory
            Player.instance.RecallMemory(associatedMemory);
            Destroy(this);
        }

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
}