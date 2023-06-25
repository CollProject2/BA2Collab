using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public PlayerMemory associatedMemory;
    public bool isActive;
    public List<Leaf> leafList = new();
    public static PlantManager instance = null;
    public Leaf currentLeaf;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        // isActive = false;

    }
    private void Update()
    {
        Player.instance.SetCanMove(false);
        if (currentLeaf != null && isActive) { CheckInput(); }
    }

    //set this as the current block
    public void SetCurrentLeaf(Leaf leaf)
    {
        currentLeaf = leaf;
    }
    public void CallCheck()
    {
        if (ShelvesAreSolved())
        {
            isActive = false;
            //recall memory
            Player.instance.RecallMemory(associatedMemory);
            Destroy(this);
        }

    }

    private void CheckInput()
    {

    }

    //rotate the block with the given index in the list
    public void RemoveLeafAt(int index)
    {
        leafList[index].RemoveLeaf();
    }

    private bool ShelvesAreSolved()
    {
        foreach (Leaf v in leafList)
            if (!v.removed) return false;
        return true;
    }
}
