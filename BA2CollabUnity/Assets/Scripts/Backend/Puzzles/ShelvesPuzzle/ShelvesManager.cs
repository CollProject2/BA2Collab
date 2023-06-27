using System.Collections.Generic;
using UnityEngine;

public class ShelvesManager : MonoBehaviour
{
    public PlayerMemory associatedMemory;
    public bool isActive;
    public List<Letter> isRight = new();
    public static ShelvesManager instance = null;
    public Letter currentLetter;

    private void Awake()
    {
        if (instance == null)
            instance = this;
       // isActive = false;

    }
    private void Update()
    {
        Player.instance.SetCanMove(false);
        if (currentLetter != null && isActive) { CheckInput(); }
    }

    //set this as the current block
    public void SetCurrentLetter(Letter letter)
    {
        currentLetter = letter;
    }
    public void CallCheck()
    {
        if (ShelvesAreSolved())
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
            RotateLetterAt(currentLetter.id, RotationLR.Left);
        else if (Input.GetKeyDown(KeyCode.D))
            RotateLetterAt(currentLetter.id, RotationLR.Right);

    }

    //rotate the block with the given index in the list
    public void RotateLetterAt(int index, RotationLR direction)
    {
        if (index >= 0 && index < isRight.Count)
            isRight[index].RotateLetter(direction);
    }

    private bool ShelvesAreSolved()
    {
        foreach (Letter v in isRight)
            if (!v.solved) return false;
        return true;
    }
}
