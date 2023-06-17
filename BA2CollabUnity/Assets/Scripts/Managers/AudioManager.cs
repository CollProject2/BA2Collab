using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    // This class is a Singleton and a Test Class 
    // https://www.youtube.com/watch?v=rcBHIOjZDpk
    // SoundSource Script is also test and created to work with this script

    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    
    private EventInstance memoryEventInstance;
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("MORE THAN ONE AUDIOMANAGER !!!");
        }
        instance = this;
        
        eventInstances = new List<EventInstance>();
        eventEmitters = new List<StudioEventEmitter>();
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound,worldPos);
    }

    public void InitializeMemoryMusic(EventReference memoryEventReference)
    {
        memoryEventInstance = CreateInstance(memoryEventReference);
        memoryEventInstance.start();
    }

    public void SetMemoryParameter(string parameterName, float parameterValue)
    {
        memoryEventInstance.setParameterByName(parameterName, parameterValue);
    }
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }
    
    private void CleanUp()
    {
        // stop and release any created instances
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        // stop all of the event emitters, because if we don't they may hang around in other scenes
        foreach (StudioEventEmitter emitter in eventEmitters)
        {
            emitter.Stop();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }
    
}
