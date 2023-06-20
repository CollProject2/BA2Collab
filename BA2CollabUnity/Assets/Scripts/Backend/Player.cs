using System.Collections.Generic;
using DG.Tweening;
using Fungus;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Singelton instance
    public static Player instance = null;
    public int missingBlocks;

    //Properties
    public Vector3 currentPosition { get; private set; }
    public List<Item> inventory { get; private set; }
    public Vector3 velocity;
    private CharacterController characterController;
    public Animator animator;

    public bool canMove;
    public bool isSolving;
    // adjust in unity editor
    public float speed;
    public float gravity;

    private Vector3 movementVector;
    private Vector3 direction;
    [SerializeField] private float smoothTime = 0.05f;
    private float currentVelocity;
    
    

    public int currentStage;


    private void Awake()
    {
        //Singelton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        canMove = false;
        isSolving = false;
        //Initialise
        characterController = GetComponent<CharacterController>();
        currentPosition = new Vector3(0, 0, 0);
        inventory = new List<Item>();
        missingBlocks = 3;
    }

    private void Update()
    {
        // Apply the movement
        HandleMovement();
        HandleRotation();
        HandleAnimation();
    }

    public void SetCanMove(bool moveState)
    {
        Debug.Log("player movement change");
        canMove = moveState;
    }

    //Methods 
    //TODO add stopping of walking while playing animation
    public void HandleMovement()
    {
        if (!canMove) return;
        if( Environment.instance.canTurnStage == false ) return;
        // Read input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        direction = new Vector3(moveX, 0.0f, moveZ);
        // movementVector = transform.right * moveX + transform.forward * moveZ;
        //
        // //normalise so all directions are the same speed (s/o to Jonas)
        // if (movementVector.magnitude > 1)
        //     movementVector = movementVector.normalized;
        //
        // movementVector = speed * Time.deltaTime * movementVector;

        // Check if character is grounded, if not add gravity
        if (characterController.isGrounded)
            velocity.y = -2f;
        else
            velocity.y -= gravity * Time.deltaTime;

        // Apply the movement
        characterController.Move(new Vector3(direction.normalized.x,direction.normalized.y,direction.normalized.z) * Time.deltaTime * speed + (velocity * Time.deltaTime));
    }

    private void HandleAnimation()
    {
        if(Environment.instance.canTurnStage == false) return;
        //animate character
        if(isSolving) return;

        if (direction.magnitude > 0)
        {
            animator.SetBool("isMoving",true);
        }
        else if (direction.magnitude <= 0)
        {
            animator.SetBool("isMoving",false);
        }
    }

    private void HandleRotation()
    {
        if(direction.sqrMagnitude == 0) return;
        
        var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothTime);
        transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
    }

    public void CollectItem(Item item)
    {
        //add item to inventory
        inventory.Add(item);
        
        SetCanMove(false);
        isSolving = true;
        // trigger pickUp anim
        animator.SetTrigger("pickUp");
        animator.SetBool("isMoving",false);
        direction = Vector3.zero;
    }

    public void SolvePuzzle(Puzzle puzzle)
    {
        // do solving puzzle things here: displaying a UI
        puzzle.StartPuzzle(puzzle, UIManager.instance.puzzleUI.blockPuzzleInstantiatePos);
        
    }
    

    public void RecallMemory()
    {
        animator.SetBool("isMoving",false);
        //hide puyyle UI 
        //UIManager.instance.puzzleUI.HideUIPuzzle(inventory[^1].associatedPuzzle.puzzleID); // inventory[^1] = the last element of the inventory list 
        //start memory
        inventory[^1].associatedPuzzle.associatedMemory.Unlock();
    }

    public void BeginNewChapter()
    {
        // checking if all parameters are true if so trigger new things
        currentStage++;
    }
}
