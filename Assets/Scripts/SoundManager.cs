using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public AudioSource musicSource;
	public AudioSource musicSource2;

	public static SoundManager instance = null;

	public float lowPitchRange = 0.95f;
	public float highPitchRange = 1.05f;

	private AudioSource[] efxStack;

	// Use this for initialization
	void Awake() {
		if(instance == null)
			instance = this;
		else if(instance != null)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

	void Update() {
		efxStack = GetComponents<AudioSource>();

		for(int i = 0; i < efxStack.Length; i++) {
			if(!efxStack[i].Equals(musicSource2) && !efxStack[i].isPlaying) {
				Destroy(efxStack[i]);
			}
		}
	}

	public void playSingle(AudioClip clip, float volume, bool bypass = false) {
		if(efxStack.Length < 7 || bypass) {
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();

			audioSource.clip = clip;
			audioSource.volume = volume;
			audioSource.Play();
		}
	}

	public void randomizeSfx(AudioClip[] clips, float volume) {	
		if(efxStack.Length < 7) {
			AudioSource audioSource = gameObject.AddComponent<AudioSource>();

			int randomIndex = Random.Range(0, clips.Length);
			float randomPitch = Random.Range(lowPitchRange, highPitchRange);

			audioSource.clip = clips[randomIndex];
			audioSource.pitch = randomPitch;
			audioSource.volume = volume;

			audioSource.Play();
		}
	}
}