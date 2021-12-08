using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorAudio : MonoBehaviour
{
    public AudioClip music, doors, movement, beep;
    private AudioSource signalsAudioSource, engineAudioSource;
    private Elevator elevator;

    private void Awake()
    {
        elevator = transform.parent.GetComponent<Elevator>();
        elevator.onDoors += PlayDoorsSound;
        elevator.onMove += PlayMusic;
        elevator.onMove += PlayEngineSound;
        elevator.onStop += StopMusic;
        elevator.onStop += StopEngineSound;
    }

    private void Start()
    {
        signalsAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        engineAudioSource = transform.GetChild(1).GetComponent<AudioSource>();        
        signalsAudioSource.clip = music;
    }

    private void PlayDoorsSound()
    {
        signalsAudioSource.PlayOneShot(doors);
    }

    private void PlayEngineSound()
    {
        if (!engineAudioSource.isPlaying)
        {
            engineAudioSource.clip = movement;
            engineAudioSource.Play();
        }
    }

    private void StopEngineSound()
    {
        engineAudioSource.Pause();
    }

    private void PlayMusic()
    {
        if (!signalsAudioSource.isPlaying)
        {
            signalsAudioSource.clip = music;
            signalsAudioSource.Play();
        }
    }

    private void StopMusic()
    {
        signalsAudioSource.clip = beep;
        signalsAudioSource.Play();
    }
}
