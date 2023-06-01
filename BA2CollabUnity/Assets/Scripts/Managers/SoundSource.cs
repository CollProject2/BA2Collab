using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class SoundSource : MonoBehaviour
{
    [SerializeField] private EventReference testSound;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.instance.PlayOneShot(testSound, this.transform.position);
        }
    }
}
