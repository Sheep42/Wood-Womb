using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundCheck : MonoBehaviour {
	public PlayerControl pController;

	void OnTriggerStay2D(Collider2D coll) {
		//On collision with ground layer
		if(!coll.isTrigger && coll.gameObject.layer == 12) {
			if(pController != null) {
				pController.grounded = true;
			}
		}
	}
}
