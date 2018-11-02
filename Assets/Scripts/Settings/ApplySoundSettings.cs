using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplySoundSettings : MonoBehaviour {

    SoundManager soundman;

	// Use this for initialization
	void Start () {
        soundman = FindObjectOfType<SoundManager>();
	}
	
	public void SetMusicVolume(float value)
    {
        soundman.SetMusicMasterVolume(value);
    }

    public void SetSFXVolume(float value)
    {
        soundman.SetSFXMasterVolume(value);
    }
}
