using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchrodController : MonoBehaviour {

	private Animator anim;

	void Awake() {
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update () {
		if(GameManager.won && anim != null) {
			anim.SetBool("gameWon", true);
		}
	}

	public void triggerStanding() {
		anim.SetBool("standing", true);
	}

	public void done() {
		anim.SetBool("done", true);
	}
}
