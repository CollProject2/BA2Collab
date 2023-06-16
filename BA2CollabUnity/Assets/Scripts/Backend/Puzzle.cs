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
    public void Awake()
    {
        Display();
    }
}

public class PuzzleTwo : Puzzle
{
    
}
public class PuzzleThree : Puzzle
{
   
}
