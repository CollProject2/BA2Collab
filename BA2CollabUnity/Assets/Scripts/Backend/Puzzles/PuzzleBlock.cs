using DG.Tweening;
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
    public GameObject faceDetector;
    public int index;
    public bool isRotating;
    public BlockFace CurrentFace;

    public bool canMoveOut = false;
    public Transform defaultBlockPos;
    public Transform defaultFaceDetectorPos;
    public Transform activeFaceDetectorPos;
    public bool interactable = false;
    //public GameObject objectModel;

    private void Start()
    {
        canMoveOut = true;
        isRotating = false;
    }

    private void OnMouseDown()
    {
        if (!interactable) return;
        if (canMoveOut)
        {
            if (BlockManager.instance.currentBlock != this)
            {
                BlockManager.instance.currentBlock.canMoveOut = true;
                BlockManager.instance.currentBlock.transform.DOMove(BlockManager.instance.currentBlock.defaultBlockPos.position, 1);
                faceDetector.transform.DOMove(defaultFaceDetectorPos.position, 1);
            }

            BlockManager.instance.SetCurrentBlock(this);
            transform.DOMove(new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f), 1);
            canMoveOut = false;
            faceDetector.transform.DOMove(activeFaceDetectorPos.position, 1);
        }

    }

    public void RotateBlock(RotationDirection direction)
    {
        if (isRotating) return;
        isRotating = true;

        Vector3 rotateAmount = Vector3.zero;
        switch (direction)
        {
            case RotationDirection.Up:
                rotateAmount = new Vector3(-90, 0, 0);
                break;
            case RotationDirection.Down:
                rotateAmount = new Vector3(90, 0, 0);
                break;
            case RotationDirection.Left:
                rotateAmount = new Vector3(0, -90, 0);
                break;
            case RotationDirection.Right:
                rotateAmount = new Vector3(0, 90, 0);
                break;
        }
        transform.DORotate(rotateAmount, 0.3f, RotateMode.WorldAxisAdd).OnComplete(() => IsDone() );

    }
    public void IsDone()
    {
        isRotating = false;
        BlockManager.instance.CallCheck(); 
    }
    private void Awake()
    {
        SetRandomRotation();
    }

    private void SetRandomRotation()
    {
        // Randomly select a face index (0-5)
        int randomFaceIndex = Random.Range(0, 6);
        Vector3 targetEulerAngles = Vector3.zero;

        switch (randomFaceIndex)
        {
            case 0: // Front face
                targetEulerAngles = new Vector3(0f, 0f, 0f);
                break;
            case 1: // Back face
                targetEulerAngles = new Vector3(0f, 180f, 0f);
                break;
            case 2: // Top face
                targetEulerAngles = new Vector3(-90f, 0f, 0f);
                break;
            case 3: // Bottom face
                targetEulerAngles = new Vector3(90f, 0f, 0f);
                break;
            case 4: // Left face
                targetEulerAngles = new Vector3(0f, -90f, 0f);
                break;
            case 5: // Right face
                targetEulerAngles = new Vector3(0f, 90f, 0f);
                break;
        }

        transform.eulerAngles = targetEulerAngles;
    }

    public void SetState(string faceName)
    {//TODO: clarification code
        switch (faceName)
        {
            case "face1":
                CurrentFace = BlockFace.Front;
                break;
            case "face2":
                CurrentFace = BlockFace.Back;
                break;
            case "face3":
                CurrentFace = BlockFace.Bottom;
                break;
            case "face4":
                CurrentFace = BlockFace.Top;
                break;
            case "face5":
                CurrentFace = BlockFace.Left;
                break;
            case "face6":
                CurrentFace = BlockFace.Right;
                break;
        }
    }
}
