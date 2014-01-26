using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public static AudioManager Singleton;

	public AudioSource[] AudioSources;
	public List<AudioClip> Music = new List<AudioClip>();
	public AudioClip[] SoundFX;

	void Start()
	{
		Singleton = this;
		OnLevelLoaded(-1);
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Update()
	{
	
	}

	public void PlayLevelMusic(int id)
	{
		switch(id){
			case 0:

			break;

			case 1:

			break;

			case 2:

			break;

			case 3:

			break;

		}
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

	public void PlaySFX(AudioClip clip,float delay)
	{
		if(clip == null){
			return;
		}

		AudioSources[2].clip = clip;
		AudioSources[2].Play();
	}
}
