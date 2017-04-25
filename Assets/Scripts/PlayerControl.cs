using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	[HideInInspector]
	public bool facingRight = true;	

	[HideInInspector]
	public bool jumping = false;

	//Horizontal movement force
	public float moveForce = 200f;

	//The max x velocity
	public float maxSpeed = 5f;	

	//Array of clips for when the player jumps.
	public AudioClip[] jumpClips;

	//Vertical movement force
	public float jumpForce = 1000f;

	//Checks for collision with the ground
	//private Transform groundCheck;
	[HideInInspector]
	public bool grounded = false;

	//Checks for collision with walls
	private Transform hero;
	private bool touchingWall = false;

	//Horz input
	private float h = 0f;

	//Phys Data for pausing/playing
	private Vector2 velocity;
	private float angularVelocity;
	private bool phyPause = false;

	private Animator anim;
	private Rigidbody2D rb2d;
	private BoxCollider2D b2d;

	void Awake() {
		// Setting up references.
		//groundCheck = transform.Find("groundCheck");
		anim = GetComponent<Animator>();
		rb2d = GetComponent<Rigidbody2D>();
		b2d = GetComponent<BoxCollider2D>();
	}

	void Update() {
		//Check if the player is touching the ground
		//grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));

		//Check if the jump button was pressed and if the player is on the ground
		if(Input.GetKey(KeyCode.Space) && grounded) {
			jumping = true;
		}

		if(GameManager.catRun)
			rb2d.isKinematic = true;
	}

	void FixedUpdate () {
		if(GameManager.won == false) {
			h = Input.GetAxis("Horizontal");

			//Trigger the walk animation
			anim.SetFloat("Speed", Mathf.Abs(h));

			//Only apply horizontal velocity if the player is not touching a wall
			if(!touchingWall) {
				//If the player is changing direction or hasn't reached maxSpeed yet
				if(h * rb2d.velocity.x < maxSpeed)
					rb2d.AddForce(Vector2.right * h * moveForce);

				//If the player's horizontal velocity is greater than the maxSpeed
				if(Mathf.Abs(rb2d.velocity.x) > maxSpeed)
					rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
			}

			if(h > 0 && !facingRight)
				Flip();
			else if(h < 0 && facingRight)
				Flip();
			
			//If the player is jumping
			if(jumping) {
				grounded = false;

				//Trigger the jump animation
				anim.SetBool("Jump", true);

				jumping = false;
			}

			velocity = rb2d.velocity;
		} else if(GameManager.catRun) {
			rb2d.velocity = new Vector2(maxSpeed, 0f);

			anim.SetFloat("Speed", 1);

			if(!facingRight)
				Flip();
		}
	}

	//Flips the player while moving
	void Flip () {
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;

		transform.localScale = theScale;
	}
		
	void OnCollisionStay2D(Collision2D coll) {
		//Don't stick to walls
		if(coll.gameObject.layer == 13) {
			if(h < 0 && coll.gameObject.name == "LeftWall")
				touchingWall = true;
			else if(h > 0 && coll.gameObject.name == "RightWall")
				touchingWall = true;
			else
				touchingWall = false;
		} else
			touchingWall = false;
	}

	void OnCollisionExit2D(Collision2D coll) {
		if(coll.gameObject.layer == 13) {
			//Don't stick to walls
			touchingWall = false;
		}
	}

	public float getVelX() {
		return velocity.x;
	}

	public float getVelY() {
		return velocity.y;
	}

	public void PhyPause() {
		phyPause = true;
		velocity = rb2d.velocity;
		angularVelocity = rb2d.angularVelocity;
		rb2d.velocity = Vector2.zero;
		rb2d.angularVelocity = 0;
		rb2d.gravityScale = 0;
		rb2d.isKinematic = true;
	}

	public void PhyPlay() {
		phyPause = false;
		rb2d.isKinematic = false; 
		rb2d.velocity = velocity;
		rb2d.angularVelocity = angularVelocity;
		rb2d.gravityScale = 1;
	}

	public void triggerJumpAction() {
		//Play a random audio clip.
		int i = Random.Range(0, jumpClips.Length);
		AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

		//Add a vertical force to the player.
		rb2d.AddForce(new Vector2(0f, jumpForce));
	}

	public void StopJump() {
		anim.SetBool("Jump", false);
	}
}