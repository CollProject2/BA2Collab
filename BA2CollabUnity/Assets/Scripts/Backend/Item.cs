using System;
using UnityEngine;
using DG.Tweening;

public class Item : MonoBehaviour
{
    // Properties
    public string itemName { get; private set; }
    public Puzzle associatedPuzzle;
    private Collider itemCollider;
    
    public GameObject modelObj;

    private void Awake()
    {
        itemCollider = gameObject.GetComponent<Collider>();
    }

    // initItem
    public void InitializeItem(string name, Puzzle puzzle)
    {
        itemName = name;
        associatedPuzzle = puzzle;
    }

    public void Display()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
    }
    public void Hide()
    {
        //gameObject.GetComponent<MeshRenderer>().enabled = false;
        //gameObject.SetActive(false);
    }

    // Methods
    public void Collect()
    {
        Player.instance.CollectItem(this); // player adds item to inventory
        itemCollider.enabled = false;
        AnimateItemOnCollect();
        Instantiate(associatedPuzzle.gameObject, transform.position, transform.rotation);
        associatedPuzzle.Display(); // display the puzzle associated with this item
        UIManager.instance.puzzleUI.DisplayUIPuzzle(associatedPuzzle.puzzleID); // Open the first UI puzzle , we just added this puzzle to the inventory so -1 to have the correct puzzle index (we start at 0)
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