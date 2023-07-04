using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public string associatedMemory;
    public bool isActive;
    public List<Leaf> leafList = new();
    public static PlantManager instance = null;
    public Item puzzleEntranceLast;
    public Leaf currentLeaf;
    public bool isComplete;
    

    private void Awake()
    {
        if (instance == null)       
            instance = this;        
        else
            Destroy(this);
        
        // isActive = false;
    }

    public void CallCheck()
    {
        if (PlantIsClean() && isActive)
        {
            isActive = false;
            Player.instance.RecallMemory(associatedMemory);
            puzzleEntranceLast.isHidden = false;
            Destroy(this);
        }
    }

    private bool PlantIsClean()
    {
        foreach (Leaf v in leafList)
            if (!v.removed) return false;
        return true;
    }
    public bool IsComplete()
    {
        return isComplete;
    }

    internal void Activate()
    {
        throw new NotImplementedException();
    }
}
