using System.Collections.Generic;
using UnityEngine;

public class Puzzle2DManager : MonoBehaviour
{
    public PlayerMemory associatedMemory;
    public List<PicturePiece> isRight = new();
    public static Puzzle2DManager instance = null;
    public PicturePiece currentPicturePiece;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        
    }
    private void Update()
    {
        if (currentPicturePiece != null ) { CheckInput(); }
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
            //recall memory
            Player.instance.RecallMemory(associatedMemory);
            DeactivatePuzzle();
            
        }

    }

    void DeactivatePuzzle()
    {
        foreach (PicturePiece v in isRight)
            v.isInteractable = false;
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
