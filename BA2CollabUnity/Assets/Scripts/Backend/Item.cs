using UnityEngine;

public class Item: MonoBehaviour
{
    // Properties
    public string itemName { get; private set; }
    public Puzzle associatedPuzzle { get; private set; }

    // Constructor
    public Item(string name, Puzzle puzzle)
    {
        this.itemName = name;
        this.associatedPuzzle = puzzle;
    }

    // Methods
    public void Collect()
    {
        Player.instance.CollectItem(this); // player adds item to inventory
        gameObject.SetActive(false); //remove item visually from gameworld, can't destroy because of the reference to this object in the player's inventory

    }
}