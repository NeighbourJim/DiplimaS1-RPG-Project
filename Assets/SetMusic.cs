using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : MonoBehaviour {

    public string SongName;

    SoundManager sm;

	// Use this for initialization
	void Start () {
        sm = FindObjectOfType<SoundManager>();
        sm.PlayMusic(SongName);
	}
	
}
