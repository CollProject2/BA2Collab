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
        // Give all object data to environment
        InitializeEnvironment();
    }

    private void InitializeEnvironment()
    {
        Environment.instance.InitializeEnvironment(items, puzzles, memories);
    }

    void InitializeGameObjects()
    {

        //THIS ONLY WORKS IF EVERY ITEM HAS ONE PUZZLE AND EVERY PUZZLE HAS ONE MEMORY; If changed this needs to be adjusted!
        //Memory first because every puzzle needs a memory, puzzle after that because every item needs a puzzle.
        for (int i = 0; i < itemPrefabs.Count; i++)
        {
            // Instantiate Memory
            PlayerMemory memory = Instantiate(memoryPrefabs[i]);
            memory.InitializePlayerMemory("Memory" + i + 1);// +1 so we start with memory 1 instead of memory 0
            memories.Add(memory);

            // Instantiate Puzzles
            Puzzle puzzle = Instantiate(puzzlePrefabs[i]);
            puzzle.InitializePuzzle("Puzzle" + i + 1, memories[i], i);
            puzzles.Add(puzzle);

            // Instantiate Items
            Item item = Instantiate(itemPrefabs[i]);
            item.InitializeItem("Item" + i + 1, puzzles[i]);
            items.Add(item);

        }

    }
}
