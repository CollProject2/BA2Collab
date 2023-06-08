using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Item> itemPrefabs; //list of all items in the game
    public List<Puzzle> puzzlePrefabs; //list of all puzzles in the game
    public List<PlayerMemory> memoryPrefabs; //list of all player memories in the game

    private List<Item> items; // List of instantiated items
    private List<Puzzle> puzzles; // List of instantiated puzzles
    private List<PlayerMemory> memories; // List of instantiated memories


    // Start is called before the first frame update
    void Start()
    {
        items = new List<Item>();
        puzzles = new List<Puzzle>();
        memories = new List<PlayerMemory>();

        // Instantiate Items, Puzzles, and Memories
        InitializeGameObjects();
    }


    void InitializeGameObjects()
    {
        //test with one of each, will add more later
        // Instantiate Items
        Item item = Instantiate(itemPrefabs[0]);

        // Instantiate Puzzles
        Puzzle puzzle = Instantiate(puzzlePrefabs[0]);

        // Instantiate Memories
        PlayerMemory memory = Instantiate(memoryPrefabs[0]);


        // assuming each item is associated with a puzzle
        item.InitializeItem("Item", puzzle);
        items.Add(item);
        // assuming each puzzle is associated with a memory
        puzzle.InitializePuzzle("memory",memory);
        puzzles.Add(puzzle);
        // assuming each memory is associated with a puzzle
        memory.InitializePlayerMemory("memory");
        memories.Add(memory);






    }
}
