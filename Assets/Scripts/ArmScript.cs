using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour {
	public Rigidbody2D player;
	public float speed = 500f;
	public float distanceToStop = 1f;

	private Vector2 direction;
	private Vector2 transformPos2D;
	private Rigidbody2D rb2d;

	void Awake() {
		rb2d = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		//if(!GameManager.won) { 
			//rb2d.velocity = new Vector2();
			direction = Vector2.zero;
			transformPos2D = new Vector2(transform.position.x, transform.position.y);

			if(Vector2.Distance(transform.position, player.position) > distanceToStop) {
				direction = player.position - transformPos2D;

				rb2d.AddRelativeForce(direction.normalized * speed, ForceMode2D.Force);
			}
		//}
	}
}
