using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ClothTest : MonoBehaviour
{
    public float endX;
    public float duration;
    private void Start()
    {
        transform.DOScaleX(endX, duration).SetEase(Ease.Linear);
    }
}
