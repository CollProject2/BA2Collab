using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantItemScript : InteractableItem
{
    public string plantMemory;
    [Header("object")]
    [SerializeField] private PlantManager plantPuzzleObj;

    protected override void Collect()
    {
        base.Collect();
        plantPuzzleObj.isIntaractable = true;
        SetIsComplete(true);
    }

}
