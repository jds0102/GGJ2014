using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public AudioSource[] AudioSources;
	public List<AudioClip> Music = new List<AudioClip>();
	public AudioClip[] SoundFX;

	void Start()
	{
		OnLevelLoaded(-1);
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Update()
	{
	
	}

	void OnLevelLoaded(int id)
	{
		if(Application.loadedLevelName == "MainMenu"){
			PlayMusic("MainTheme");
		}
	}

	void PlayMusic(string music)
	{
		foreach(AudioClip song in Music){
			if(song.name == music){
				AudioSources[0].clip = song;
				AudioSources[0].loop = true;
				AudioSources[0].Play();
			}
		}

	}
}
