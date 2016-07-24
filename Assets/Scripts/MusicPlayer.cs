using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {
	static MusicPlayer instance = null;

	public AudioClip startMusic;
	public AudioClip gameMusic;
	public AudioClip endMusic;

	private AudioSource music;
	
	void Start () {
		if (instance != null && instance != this) {
			Destroy (gameObject);
			print ("Duplicate music player self-destructing!");
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			music = GetComponent<AudioSource> ();
			music.clip = startMusic;
			music.loop = true;
			music.Play ();
		}
		
	}

	void OnLevelWasLoaded(int level){

		music.Stop ();

		if (level == 0) {
			music.clip = startMusic;
		} 
		if (level == 1) {
			music.clip = gameMusic;
		} 
		if (level == 2) {
			music.clip = endMusic;
		}

		music.loop = true;
		music.Play ();

	}
}
