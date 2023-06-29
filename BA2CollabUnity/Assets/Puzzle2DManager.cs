using System.Collections.Generic;
using UnityEngine;

public class Puzzle2DManager : MonoBehaviour
{
    public PlayerMemory associatedMemory;
    public bool isActive;
    public List<PicturePiece> isRight = new();
    public static Puzzle2DManager instance = null;
    public PicturePiece currentPicturePiece;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        // isActive = false;

    }
    private void Update()
    {
        Player.instance.SetCanMove(false);
        if (currentPicturePiece != null && isActive) { CheckInput(); }
    }

    //set this as the current block
    public void SetCurrentPicturePiece(PicturePiece currentPP)
    {
        currentPicturePiece = currentPP;
    }
    public void CallCheck()
    {
        if (PictureIsSolved())
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
            RotatePPAt(currentPicturePiece.id, RotationLR.Left);
        else if (Input.GetKeyDown(KeyCode.D))
            RotatePPAt(currentPicturePiece.id, RotationLR.Right);

    }

    //rotate the block with the given index in the list
    public void RotatePPAt(int index, RotationLR direction)
    {
        if (index >= 0 && index < isRight.Count)
            isRight[index].RotatePicturePiece(direction);
    }

    private bool PictureIsSolved()
    {
        foreach (PicturePiece v in isRight)
            if (!v.solved) return false;
        return true;
    }
}
