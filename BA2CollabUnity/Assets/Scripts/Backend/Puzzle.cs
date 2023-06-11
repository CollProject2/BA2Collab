using Unity.VisualScripting;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    //Properties
    public bool isSolved { get; set; }
    public string puzzleName { get; private set; }
    public int puzzleID { get; private set; }
    public PlayerMemory associatedMemory { get; private set; }

    //Init
    private void Awake()
    {
        isSolved = false;
        Hide();
    }

    //constructor for puzzle todo
    public void InitializePuzzle(string name, PlayerMemory memory, int puzzleNumber)
    {
        puzzleName = name;
        associatedMemory = memory;
        puzzleID = puzzleNumber;

    }

    //Methods
    public void Display()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void StartPuzzle()
    {
        //GetComponentInChildren<MeshRenderer>().enabled = true;

        //select right puzzle
        switch (puzzleID)
        {
            case 0:
                this.AddComponent<PuzzleOne>();
                break;
            case 1:
                this.AddComponent<PuzzleTwo>();
                break;
            case 2:
                this.AddComponent<PuzzleTwo>();
                break;
            default:
                break;
        }

    }
    public void Solve()
    {
        //when solved, unlock a memory 
        isSolved = true;

        //let player handle the rest
        Player.instance.RecallMemory();
    }


}

public class PuzzleOne : Puzzle
{
    private int targetNumber;
    private int playerGuess;
    private bool isActive;

    private void Awake()
    {
        // Generate a random target number between 1 and 3
        targetNumber = Random.Range(1, 3);
        isActive = false;
        ExecutePuzzle();
    }
    private void Update()
    {
        if (!isSolved && isActive)
            ExecutePuzzle();
    }

    public void ExecutePuzzle()
    {
        isActive = true;
        // Read player's input (for simplicity, we're using number keys)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            playerGuess = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            playerGuess = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            playerGuess = 3;
        else
            return;

        if (playerGuess == targetNumber)
        {
            // Player has guessed the correct number
            isActive = false;
            Solve();
            //Destroy(this);
        }
    }
}

public class PuzzleTwo : Puzzle
{
    private int targetNumber;
    private int playerGuess;
    private bool isActive;

    private void Awake()
    {
        // Generate a random target number between 1 and 3
        targetNumber = Random.Range(1, 3);
        isActive = false;
        ExecutePuzzle();
    }
    private void Update()
    {
        if (!isSolved && isActive)
            ExecutePuzzle();
    }

    public void ExecutePuzzle()
    {
        isActive = true;
        // Read player's input (for simplicity, we're using number keys)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            playerGuess = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            playerGuess = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            playerGuess = 3;
        else
            return;

        if (playerGuess == targetNumber)
        {
            // Player has guessed the correct number
            isActive = false;
            Solve();
            //Destroy(this);
        }
    }
}
public class PuzzleThree : Puzzle
{
    private int targetNumber;
    private int playerGuess;
    private bool isActive;

    private void Awake()
    {
        // Generate a random target number between 1 and 3
        targetNumber = Random.Range(1, 3);
        isActive = false;
        ExecutePuzzle();
    }
    private void Update()
    {
        if (!isSolved && isActive)
            ExecutePuzzle();
    }

    public void ExecutePuzzle()
    {
        isActive = true;
        // Read player's input (for simplicity, we're using number keys)
        if (Input.GetKeyDown(KeyCode.Alpha1))
            playerGuess = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            playerGuess = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            playerGuess = 3;
        else
            return;

        if (playerGuess == targetNumber)
        {
            // Player has guessed the correct number
            isActive = false;
            Solve();
            //Destroy(this);
        }
    }
}
