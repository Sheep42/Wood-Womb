using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {
	//public int hitPoints = 100;
	public AudioClip doorBreak;
	public cameraController camController;

	private Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
	}

	void Update() {
		if(GameManager.won) {
			anim.SetBool("gameWon", true);
		}
	}

	public void playSoundAndShake() {
		SoundManager.instance.playSingle(doorBreak, 0.8f, true);
		camController.shakeCam(0.1f, 0.3f, 0.1f);

		GameManager.catRun = true;
	}

	public void done() {
		anim.SetBool("done", true);
	}
}
