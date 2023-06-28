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
        else
        {
            Debug.LogError("PlantManager already exists");
            Destroy(this);
        }
        // isActive = false;
    }

    public void CallCheck()
    {
        if (PlantIsClean() && isActive)
        {
            isActive = false;
            Player.instance.RecallMemory(associatedMemory);
            Destroy(this);
        }
    }

    private bool PlantIsClean()
    {
        foreach (Leaf v in leafList)
            if (!v.removed) return false;
        return true;
    }
}
