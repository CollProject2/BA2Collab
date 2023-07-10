using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using Fungus;

public class AudioManager : MonoBehaviour
{
    // This class is a Singleton and a Test Class 
    // https://www.youtube.com/watch?v=rcBHIOjZDpk
    // SoundSource Script is also test and created to work with this script

    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
    
    private EventInstance memoryEventInstance;

    [Header("Parameters")] 
    public float textSoundAge = 0;

    public bool CAN_PLAY_DIALOG;
    
    //Singleton
    public static AudioManager instance { get; private set; }

    private void Start()
    {
        textSoundAge = 0;
    }

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

    public void SetTextSoundAge(int age)
    {
        textSoundAge = age;
    }
    
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }
    // get path of event
    public string GetInstantiatedEventName(FMOD.Studio.EventInstance instance)
    {
        string result;
        FMOD.Studio.EventDescription description;

        instance.getDescription(out description);
        description.getPath(out result);

        // expect the result in the form event:/folder/sub-folder/eventName
        return result; 

    }
    
    // for instances that will be destroyed quickly;
    public EventInstance CreateUnlistedInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
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
