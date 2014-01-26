using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {

	public static AudioManager Singleton;

	public AudioSource[] AudioSources;
	public List<AudioClip> Music = new List<AudioClip>();
	public List<AudioSource> levelTracks;
	public AudioClip[] SoundFX;

	private float fadeStart;
	private int fadeIn, fadeOut;
	private float fadeTime;
	private bool fading;

	void Start()
	{
		Singleton = this;
		OnLevelLoaded(-1);
		DontDestroyOnLoad(this.gameObject);
	}
	
	void Update()
	{
		float timeSinceFadeStart = Time.time - fadeStart;
		if (timeSinceFadeStart < fadeTime) {
			levelTracks[fadeIn].volume = timeSinceFadeStart/fadeTime;
			levelTracks[fadeOut].volume = 1 - timeSinceFadeStart/fadeTime;
		} else if (fading) {
			fading = false;
			levelTracks [fadeIn].volume = 1;
			levelTracks [fadeOut].volume = 0;
			levelTracks [fadeOut].Stop();
		}
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

	public void PlaySFX(AudioClip clip)
	{
		if(clip == null){
			return;
		}

		AudioSources[2].clip = clip;
		AudioSources[2].Play();
	}

	public void FadeBetweenLevels(int prevLevel, int nextLevel, float fadeDuration) {
		fading = true;

		fadeStart = Time.time;
		fadeIn = nextLevel;
		fadeOut = prevLevel;
		fadeTime = fadeDuration;

		levelTracks [fadeIn].volume = 0;
		levelTracks [fadeIn].loop = true;
		levelTracks [fadeIn].Play ();

	}
}
