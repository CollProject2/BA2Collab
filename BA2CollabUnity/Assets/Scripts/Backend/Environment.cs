using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    //Singelton instance
    public static Environment instance = null;
    //Properties
    public List<Item> items { get; private set; }
    public List<Puzzle> puzzles { get; private set; }
    public List<PlayerMemory> memory { get; private set; }
    public float rotationAngle { get; private set; }
    public List<Vector3> rotationPoints { get; private set; }

    //Init
    public void InitializeEnvironment(List<Item> itemList, List<Puzzle> puzzleList, List<PlayerMemory> memoryList)
    {
        items = itemList;
        puzzles = puzzleList;
        memory = memoryList;
        rotationAngle = 0.0f;
        rotationPoints = new List<Vector3>();
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

        //make lists new init here so they are empty on awake
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

    public void Rotate(float newAngle)
    {
        rotationAngle = newAngle;
    }

    public void CheckForRotationPoint(Vector3 playerPosition)
    {
        foreach (var e in rotationPoints)
        {
            if (playerPosition == e)
                Rotate(5);//test value
        }
    }
}
