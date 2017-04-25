using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackTrigger : MonoBehaviour {
	public float hitForce = 10f;
	public PlayerControl pController;

	private Rigidbody2D rb2d;

	void OnTriggerEnter2D(Collider2D coll) {
		if(!coll.isTrigger && coll.CompareTag("Collider")) {
			rb2d = coll.GetComponent<Rigidbody2D>();

			if(rb2d != null) {
				//Calculate the force to exert on the collider
				float xForce = (pController.facingRight) 
					? (pController.getVelX() / 2) + (hitForce * Random.Range(1.0f, 2.1f)) 
					: (pController.getVelX() / 2) + (-hitForce * Random.Range(1.0f, 2.1f));
				
				float yForce = hitForce * Random.Range(1.0f, 2.1f);

				//Hit the collider
				Vector2 hitVel = new Vector2(xForce, yForce);
				rb2d.AddForce(hitVel, ForceMode2D.Impulse);
			}
		}
	}
}
