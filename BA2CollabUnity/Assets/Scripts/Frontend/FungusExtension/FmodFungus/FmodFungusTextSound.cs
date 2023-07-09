using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using Fungus;
using UnityEngine;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;
[CommandInfo("Fmod","textSfx","Play text sound effect while writing")]
public class FmodFungusTextSound : Command
{
    // [field: Header("TextSound")]
    // [field: SerializeField] public EventReference textSfx { get; private set; }
    //
    // private EventInstance textSfxEvent;
    //
    // private PLAYBACK_STATE playbackState;
    //
    // public Writer writer;
    //
    // public void Start()
    // {
    //     textSfxEvent = AudioManager.instance.CreateInstance(textSfx);
    //     textSfxEvent.getPlaybackState(out playbackState);
    // }

    public override void OnEnter()
    {
        // if (writer.IsWriting && !writer.IsWaitingForInput)
        // {
        //     if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
        //     {
        //         textSfxEvent.start();
        //     }
        // }
        Continue();
    }

    // private void Update()
    // {
    //     textSfxEvent.setParameterByName("TextSoundAge", AudioManager.instance.textSoundAge);
    //     
    //     if (writer.IsWriting)
    //     {
    //         if (writer.IsWaitingForInput)
    //         {
    //             textSfxEvent.stop(STOP_MODE.IMMEDIATE);
    //             playbackState = PLAYBACK_STATE.STOPPED;
    //         }
    //         else if (!writer.IsWaitingForInput)
    //         {
    //             if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
    //             {
    //                 textSfxEvent.start();
    //                 playbackState = PLAYBACK_STATE.PLAYING;
    //             }
    //         }
    //     }
    //     else
    //     {
    //         textSfxEvent.stop(STOP_MODE.IMMEDIATE);
    //         playbackState = PLAYBACK_STATE.STOPPED;
    //     }
    //     
    // }
}
