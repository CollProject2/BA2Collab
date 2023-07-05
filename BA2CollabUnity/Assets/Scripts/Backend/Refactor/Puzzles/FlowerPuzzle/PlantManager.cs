using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public string associatedMemory;
    public bool isIntaractable;
    public List<Leaf> leafList = new();
    public static PlantManager instance = null;
    public Leaf currentLeaf;
    public bool isComplete;
    

    private void Awake()
    {
        if (instance == null)       
            instance = this;        
        else
            Destroy(this);
        
        isIntaractable = false;
    }

    public void CallCheck()
    {
        if (PlantIsClean() && isIntaractable)
        {
            isIntaractable = false;
            UIManager.instance.dialogues.StartDialogue(associatedMemory);
            isComplete = true;
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
        isIntaractable = true;
    }
}
