using System;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    // Properties
    public string itemName { get; private set; }
    public Puzzle associatedPuzzle { get; private set; }
    public PuzzleUI associatedPuzzleUI { get; private set; }
    private Collider collider;
    
    public GameObject modelObj;

    private void Awake()
    {
        collider = gameObject.GetComponent<Collider>();
    }

    // initItem
    public void InitializeItem(string name, Puzzle puzzle)
    {
        itemName = name;
        associatedPuzzle = puzzle;
    }

    // Methods
    public void Collect()
    {
        Player.instance.CollectItem(this); // player adds item to inventory
        //gameObject.SetActive(false); //remove item visually from gameworld, can't destroy because of the reference to this object in the player's inventory
        collider.enabled = false;
        AnimateItemOnCollect();
        associatedPuzzle.Display(); // display the puzzle associated with this item
        UIManager.instance.puzzleUI.DisplayUIPuzzle(0); // Open the first UI puzzle 
        Player.instance.SolvePuzzle(associatedPuzzle); // player attempts to solve the puzzle associated with this item

    }
    
    void HideItemModel()
    {
        modelObj.SetActive(true);
        
    }
    public void AnimateItemOnCollect()
    {
        modelObj.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f).OnComplete(() =>
        {
            modelObj.transform.DOScale(new Vector3(0, 0, 0), 0.5f).OnComplete(HideItemModel);
        });
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }

    //specific item stuff
}