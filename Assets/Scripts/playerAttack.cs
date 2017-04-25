using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour {

	public Collider2D attackTrigger;

	//The attack button
	public KeyCode attKey;

	[HideInInspector]
	public bool attacking = false;

	private float attackTimer = 0f;
	private float attackCoolDown = 0.3f;

	private Animator anim;

	void Awake() {
		anim = gameObject.GetComponent<Animator>();
		attackTrigger.enabled = false;
	}

	void Update() {
		if((Input.GetKeyDown(attKey) && !attacking)) {
			anim.SetBool("Attack", true);

			attacking = true;
			attackTimer = attackCoolDown;

			attackTrigger.enabled = true;
		}

		if(attacking) {
			if(attackTimer > 0) {
				attackTimer -= Time.deltaTime;
			} else {
				attacking = false;
				attackTrigger.enabled = false;
			}
		}
	}

	public void stopAttack() {
		anim.SetBool("Attack", false);
	}
}
