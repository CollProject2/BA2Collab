using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum RotationDirection
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}
public enum BlockFace
{
    Front,
    Back,
    Left,
    Right,
    Top,
    Bottom
}

public class PuzzleBlock : MonoBehaviour
{
    private static Dictionary<RotationDirection, Vector3> rotationDirections = new()
    {
        {RotationDirection.Up, new Vector3(-90, 0, 0)},
        {RotationDirection.Down, new Vector3(90, 0, 0)},
        {RotationDirection.Left, new Vector3(0, -90, 0)},
        {RotationDirection.Right, new Vector3(0, 90, 0)},
    };

    private static Dictionary<string, BlockFace> faceNameToBlockFace = new()
    {
        {"face1", BlockFace.Front},
        {"face2", BlockFace.Back},
        {"face3", BlockFace.Bottom},
        {"face4", BlockFace.Top},
        {"face5", BlockFace.Left},
        {"face6", BlockFace.Right}
    };

    public GameObject faceDetector;
    public BlockFace CurrentFace;

    private bool isRotating;
    private bool canMoveOut;
    public bool interactable;

    public int index;

    public Transform defaultBlockPos;
    public Transform defaultFaceDetectorPos;
    public Transform activeFaceDetectorPos;


    private void Start()
    {
        //init variables 
        canMoveOut = true;
        isRotating = false;
        interactable = false;
    }

    private void Awake()
    {
        SetRandomRotation();
    }

    //when the player interract with the collider of the block
    private void OnMouseDown()
    {
        //prevent clicking before the puzzle is loaded
        if (!interactable) return;

        //prevent double clicking a block
        if (canMoveOut)
        {
            //Set the old block back into original position if the block we clicked on is not this block
            if (BlockManager.instance.currentBlock != this)
            {
                BlockManager.instance.currentBlock.canMoveOut = true;
                //move the block away
                BlockManager.instance.currentBlock.transform.DOMove(BlockManager.instance.currentBlock.defaultBlockPos.position, 1);
                //move the detector too so it can still detect
                faceDetector.transform.DOMove(defaultFaceDetectorPos.position, 1);
            }
            //set the new block as the current block
            BlockManager.instance.SetCurrentBlock(this);
            //move the block towards player
            transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), 1);
            canMoveOut = false;
            //move facedetector 
            faceDetector.transform.DOMove(activeFaceDetectorPos.position, 1);
        }

    }

    //rotate the block 
    public void RotateBlock(RotationDirection direction)
    {
        //if its rotating we don't rotate
        if (isRotating) return;
        //we set rotating to true
        isRotating = true;
        //rotate and will call the wincon check and turn rotating false after the rotation is complete
        transform.DORotate(rotationDirections[direction], 0.3f, RotateMode.WorldAxisAdd).OnComplete(IsDone);
    }

    //function that gets called once the rotating of the block is done
    public void IsDone()
    {
        isRotating = false;
        BlockManager.instance.CallCheck();
    }

    //set the puzzle random to start 
    private void SetRandomRotation()
    {
        float yRotation = Random.Range(0, 4) * 90f;
        float zRotation = Random.Range(0, 2) * 180f;
        transform.eulerAngles = new Vector3(0f, yRotation, zRotation);
    }

    //set the state of the block to the given name
    public void SetState(string faceName)
    {
        CurrentFace = faceNameToBlockFace[faceName];
    }
}
