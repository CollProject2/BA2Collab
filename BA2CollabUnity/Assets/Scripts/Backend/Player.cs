using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Singelton instance
    public static Player instance = null;

    //Properties
    public Vector3 currentPosition { get; private set; }
    public List<Item> inventory { get; private set; }
    public Vector3 velocity;
    private CharacterController characterController;
    // adjust in unity editor
    public float speed;
    public float gravity;

    public int currentStage;

    //Constructor
    public Player()
    {
        currentPosition = new Vector3(0, 0, 0);
        inventory = new List<Item>();
    }

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
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        // Apply the movement
        HandleMovement();
    }

    //Methods
    public void HandleMovement()
    {
        // Read input for horizontal and vertical movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");


        Vector3 movementVector = transform.right * moveX + transform.forward * moveZ;

        //normalise so all directions are the same speed (s/o to Jonas)
        if (movementVector.magnitude > 1)
            movementVector = movementVector.normalized;

        movementVector = speed * Time.deltaTime * movementVector;

        // Check if character is grounded, if not add gravity
        if (characterController.isGrounded)
            velocity.y = -2f;
        else
            velocity.y -= gravity * Time.deltaTime;

        // Apply the movement
        characterController.Move(movementVector + (velocity * Time.deltaTime));
    }

    public void CollectItem(Item item)
    {
        //add item to inventory
        inventory.Add(item);
    }

    public void SolvePuzzle(Puzzle puzzle)
    {
        // do solving puzzle things here: displaying a UI
        puzzle.SelectPuzzle();
    }

    public void RecallMemory(PlayerMemory memory)
    {
        memory.Unlock();
        // recalling memory => playing an animation or cutscene, or displaying some text or images
    }

    public void BeginNewChapter()
    {

        // checking if all parameters are true if so trigger new things
        currentStage++;
    }
}
