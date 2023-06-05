using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Singelton instance
    public static Player instance = null;

    //Properties
    public Vector3 currentPosition { get; private set; }
    public List<Item> inventory { get; private set; }
    
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
    }

    //Methods
    public void Move(Vector3 newPosition)
    {
        // TODO: Check if newPosition is valid + character controller/rigid body stuff
        currentPosition = newPosition;
    }

    public void CollectItem(Item item)
    {
        //add item to inventory
        inventory.Add(item);
    }

    public void SolvePuzzle(Puzzle puzzle)
    {
        // do solving puzzle things here: displaying a UI, waiting for user input and then checking the solution 
        //when solved, unlock a memory 
    }

    public void RecallMemory(PlayerMemory memory)
    {
        // recalling memory => playing an animation or cutscene, or displaying some text or images
    }

    public void BeginNewChapter()
    {
        // checking if all parameters are true if so trigger new things
    }
}
