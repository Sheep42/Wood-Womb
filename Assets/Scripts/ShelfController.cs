using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfController : Destructable {

	public cameraController camController;
	public int jumpNum = 3;
	public float standTimer = 3f;
	public AudioClip shelfBreak;

	private int jumpTimes = 0;
	private float initTimer = 0f;
	private bool fallen = false;

	//Components
	private Rigidbody2D rb2d;
	private BoxCollider2D bc2d;

	void Awake() {
		bc2d = GetComponent<BoxCollider2D>();

		rb2d = GetComponent<Rigidbody2D>();
		rb2d.bodyType = RigidbodyType2D.Static;

		initTimer = standTimer;
	}

	void FixedUpdate () {
		//The platform falling...
		if((jumpTimes >= jumpNum || standTimer <= 0) && !fallen) {
			SoundManager.instance.playSingle(shelfBreak, 0.3f);

			bc2d.usedByEffector = false;
			rb2d.bodyType = RigidbodyType2D.Dynamic;

			tag = "Collider";

			fallen = true;
			camController.shakeCam(0.1f, 0.3f, 0.1f);

			GameManager.destructionPoints += 5;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		base.OnCollisionEnter2D(coll);

		Collider2D collider = coll.collider;

		//Fall if the player jumps too many times
		if(!fallen && collider != null && collider.name == "hero") {
			Vector3 contactPoint = coll.contacts[0].point;
			Vector3 center = collider.bounds.center;

			bool shelfTop = contactPoint.y < center.y;

			if(shelfTop) {
				jumpTimes += 1;
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		//Fall if the player stands for too long
		if(!fallen && coll.collider.name == "hero") {
			standTimer -= Time.deltaTime;
		}
	}

	void OnCollisionExit2D(Collision2D coll) {
		//Reset the timer
		if(!fallen && standTimer > 0)
			standTimer = initTimer;
	}
}
