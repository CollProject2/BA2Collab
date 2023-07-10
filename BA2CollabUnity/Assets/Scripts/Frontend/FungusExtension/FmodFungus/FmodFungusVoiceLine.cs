using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using Fungus;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

[CommandInfo("Fmod", "VoiceLine", "unskipable voiceline")]
public class FmodFungusVoiceLine : Command
{
    private DialogInput dialogInput;

    public EventReference voiceLine;
    private EventInstance voiceLineInstance;
    private EventDescription eventDescription;

    private void Awake()
    {
        dialogInput = GameObject.Find("SayDialog").GetComponent<DialogInput>();
    }

    public override void OnEnter()
    {
        if (AudioManager.instance.CAN_PLAY_DIALOG)
        {
            dialogInput.enabled = false;
            voiceLineInstance = AudioManager.instance.CreateUnlistedInstance(voiceLine);
            voiceLineInstance.start();
        }

        Continue();
    }

    // check is it playing 
    bool IsPlaying(EventInstance instance)
    {
        PLAYBACK_STATE state;
        instance.getPlaybackState(out state);
        return state != PLAYBACK_STATE.STOPPED;
    }

    private void Update()
    {
        if (AudioManager.instance.CAN_PLAY_DIALOG)
        {
            if (!voiceLineInstance.isValid()) return;
            // check if the event exist, if not return. if 
            if (RuntimeManager.StudioSystem.getEvent(AudioManager.instance.GetInstantiatedEventName(voiceLineInstance),
                    out eventDescription) == RESULT.ERR_EVENT_NOTFOUND)
            {
                return;
            }
            else
            {
                // if the event exists and it is not playing anymore, continue to the next command by allowing the click and remove event
                if (!IsPlaying(voiceLineInstance))
                {
                    dialogInput.enabled = true;
                    voiceLineInstance.stop(STOP_MODE.IMMEDIATE);
                    voiceLineInstance.release();
                }
            }
        }
    }
}