using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    // This class is a Singleton and a Test Class 
    // https://www.youtube.com/watch?v=rcBHIOjZDpk
    // SoundSource Script is also test and created to work with this script
    
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("MORE THAN ONE AUDIOMANAGER !!!");
        }
        instance = this;
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound,worldPos);
    }
}
