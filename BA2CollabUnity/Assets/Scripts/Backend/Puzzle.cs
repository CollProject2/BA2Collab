using UnityEngine;

public class Puzzle: MonoBehaviour
{
    //Properties
    public bool isSolved { get; set; }
    public PlayerMemory associatedMemory { get; private set; }

    //Constructor
    public Puzzle(PlayerMemory memory)
    {
        associatedMemory = memory;
        isSolved = false;
    }

    //Methods
    public void Display()
    {
        //show the puzzle to the player (e.g. display a picture)
    }

    public void Solve()
    {
        // call the solve method of the player
        Player.instance.SolvePuzzle(this);
    }
}