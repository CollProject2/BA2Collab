using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    public Animator animator;
    public static Player instance = null;
    public GameObject playerModel;

    private Vector3 direction;
    public Vector3 velocity;
    public Transform playerCarrypos;
    public Vector3 currentPosition { get; private set; }

    //public List<Item> inventory { get; private set; }

    [SerializeField]
    private float smoothTime;
    private float currentVelocity;
    public float speed;
    public float gravity;

    public bool canMove;
    public bool isSolving;
    public bool hasPoster;
    public bool hasGlasses;
    public bool hasBear;
    public bool hasMovingBox;
    public bool cutscene;

    public bool inGarden;

    public int currentStage;
    public int missingBlocks;

    private readonly Vector3 initialPosition = new(0, 0, 0);
    private readonly float initialVelocityY = -2f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        //init variables 
        hasPoster = false;
        smoothTime = 0.05f;
        canMove = false;
        isSolving = false;
        characterController = GetComponent<CharacterController>();
        currentPosition = initialPosition;
        //inventory = new List<Item>();
        missingBlocks = 3;
        inGarden = false;
        cutscene = false;
    }

    private void Update()
    {
        if (!canMove || !Environment.instance.canTurnStage || cutscene)
            return;

        HandleMovement();
        HandleRotation();
        HandleAnimation();
    }

    public void SetCanMove(bool moveState)
    {
        canMove = moveState;
        if(moveState == false)
            animator.SetBool("isMoving", moveState);
    }

    public void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        direction = new Vector3(moveX, 0.0f, moveZ);
        //gravity
        if (characterController.isGrounded)
            velocity.y = initialVelocityY;
        else
            velocity.y -= gravity * Time.deltaTime;
        //move
        if (characterController.enabled)
            characterController.Move(speed * Time.deltaTime * direction.normalized + (velocity * Time.deltaTime));
    }

    private void HandleAnimation()
    {
        //no animation while solving
        if (isSolving) return;

        //set is moving
        animator.SetBool("isMoving", direction.magnitude > 0);
    }

    private void HandleRotation()
    {
        if (direction.sqrMagnitude == 0) return;

        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    public float CheckDistanceWithPlayer(Vector3 position)
    {
        return Vector3.Distance(Player.instance.transform.position, position);
    }


    public void RecallMemory(string memory)
    {
        SetCanMove(false);
        UIManager.instance.dialogues.StartDialogue(memory);
    }

    public void BeginNewChapter()
    {
        currentStage++;
    }

    public void TeleportPlayer(Transform tpPos)
    {
        characterController.enabled = false;
        transform.position = tpPos.position;
        characterController.enabled = true;
    }

    public void SetCharacterController(bool state)
    {
        characterController.enabled = state;
    }

    public void MoveCharacterAwayCutscene()
    {
        cutscene = true;
        transform.position = new Vector3(-1.89999998f, 1.42299998f, -2.20700002f);
        playerModel.SetActive(false);
        characterController.enabled = false;
    }
}
