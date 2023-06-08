using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    //Properties
    public List<Item> items { get; private set; }
    public List<Puzzle> puzzles { get; private set; }
    public List<PlayerMemory> memory { get; private set; }
    public float rotationAngle { get; private set; }
    public List<Vector3> rotationPoints { get; private set; }

    //Constructor
    public Environment()
    {
        items = new List<Item>();
        puzzles = new List<Puzzle>();
        memory = new List<PlayerMemory>();
        rotationAngle = 0.0f;
        rotationPoints = new List<Vector3>();
    }

    //Methods
    public void Display()
    {
        //show everything in the environment
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
