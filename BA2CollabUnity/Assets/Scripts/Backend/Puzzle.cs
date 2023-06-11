using TMPro;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    //Properties
    public bool isSolved { get; set; }
    public string puzzleName { get; private set; }
    public PlayerMemory associatedMemory { get; private set; }

    private void Update()
    {
        if (!isSolved)
            FirstPuzzle(); //hardcoded for now
    }
    //Init
    private void Awake()
    {
        isSolved = false;
        Hide();
    }

    public void InitializePuzzle(string name, PlayerMemory memory)
    {
        associatedMemory = memory;
        puzzleName = name;
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

    public void Solve()
    {
        //when solved, unlock a memory 
        isSolved = true;
        Player.instance.RecallMemory(associatedMemory);
        Hide();
        UIManager.instance.puzzleUI.HideUIPuzzle(0);
    }

    // Additional properties for the first puzzle
    private int targetNumber;
    private int playerGuess;

    public void SelectPuzzle()
    {
        //for now its all hardcoded

        GetComponentInChildren<MeshRenderer>().enabled=true;
        // Generate a random target number between 1 and 3
        targetNumber = Random.Range(1, 3);
        FirstPuzzle();
    }


    //specific puzzle stuff
    private void FirstPuzzle()
    {

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
            Solve();
        }
    }
}