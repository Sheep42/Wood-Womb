using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour {
	public cameraController camController;
		
	void Update() {
		if(GameManager.won) {
			if(gameObject.name == "RightWall") {
				GetComponent<BoxCollider2D>().isTrigger = true;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if(coll.gameObject != null && coll.gameObject.CompareTag("Collider")) {
			camController.shakeCam(0.1f, 0.3f, 0.1f);
		}
	}
}
