using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//Components
	private Rigidbody2D rigidBody;
	private Animator animator;
	private SoundManager soundManager;

	//Attributes
	public float horizontalSpeed;
	public float verticalSpeed;
	private float currentSpeed = 3;
	private float currentSpeedNegative = -3;
	private bool facingRight = true;
	private bool isMining = false;

	//attributes to check if the player is falling
	public bool isFalling = false;

	//debugging
	public float horizontal;
	public float vertical;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		soundManager = GameObject.Find ("SoundManager").GetComponent<SoundManager> ();
		facingRight = true;
		isFalling = false;


	}

	void Update ()
	{
	}

	//fixedupdate is called just before performing physic calculations -> movement comes here (no Time.deltatime)
	void FixedUpdate ()
	{
		if (!isMining && !isFalling) {

			float newSpeed;

			//Keys are set in the InputManager by default
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");

			//debugging
			horizontal = moveHorizontal;
			vertical = moveVertical;

			//set speed for walking animation - mathf.abs for positive value only
			animator.SetFloat ("speed", Mathf.Abs (moveHorizontal));
			if (moveHorizontal > 0 || moveHorizontal < 0)
				soundManager.PlayFootstep ();

			//check for maxSpeed - can be upgraded later
			newSpeed = moveHorizontal * horizontalSpeed;
			if (newSpeed >= currentSpeed)
				newSpeed = currentSpeed;
			else if (newSpeed <= currentSpeedNegative)
				newSpeed = currentSpeedNegative;

			//add forces to the rigidBody2D for movement
			rigidBody.AddForce (new Vector2 (newSpeed, moveVertical * verticalSpeed));

			//If we moving left and facing right -> flip, if we move right and not facing right -> flip
			if (moveHorizontal > 0 && !facingRight)
				FlipSprite ();
			else if (moveHorizontal < 0 && facingRight)
				FlipSprite ();
		}
	}

	//Easy way to flip sprite for 180 degree
	void FlipSprite ()
	{
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}



	public float GetCurrentSpeed ()
	{
		return currentSpeed;
	}

	public void SetCurrentSpeed (float newSpeed)
	{
		currentSpeed = newSpeed;
	}


	void OnCollisionStay2D (Collision2D collision)
	{
		//is the player falling?
		if (rigidBody.velocity.y < -0.1) {
			isFalling = true;
		} else {
			isFalling = false;
		}
		if (!isMining && !isFalling) {
			//Keys are set in the InputManager by default
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			horizontal = moveHorizontal;
			vertical = moveVertical;
	
			Vector3 pos = this.gameObject.transform.position;
			Vector3 comparePos = collision.transform.position;

			if (vertical != 0)
				horizontal = 0;
			if (horizontal != 0)
				vertical = 0;
	
			if (collision.gameObject.tag == "Resource") {
				//check if mineable 

				// the player should only mine at specific points -> prevent mining at edge eg.
				if (vertical < 0) {
					if (comparePos.y < pos.y && Mathf.Abs (comparePos.x - pos.x) < 0.3) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}
				
				} else if (horizontal < 0) {
					if (comparePos.x < pos.x && Mathf.Abs (comparePos.y - pos.y) < 0.3) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}
				
				} else if (horizontal > 0) {
					if (comparePos.x > pos.x && Mathf.Abs (comparePos.y - pos.y) < 0.3) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}
				}

			} 

			if (collision.gameObject.tag == "Tile") {
				//what to do if the object is a tile
				if (vertical < 0) {
					if (comparePos.y < pos.y && Mathf.Abs (comparePos.x - pos.x) < 0.3) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}

				} else if (horizontal < 0) {
					if (comparePos.x < pos.x && Mathf.Abs (comparePos.y - pos.y) < 0.3) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}

				} else if (horizontal > 0) {
					if (comparePos.x > pos.x && Mathf.Abs (comparePos.y - pos.y) < 0.3) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}
				}
			}
		}
	}

	IEnumerator WaitForTime (Collision2D collision, float time)
	{
		//animation etc.
		//Play sound and stop animations
		soundManager.PlayMine ();
		animator.SetFloat ("speed", 0f);
		//player is mining -> boolean prevents for other activities
		isMining = true;
		yield return new WaitForSeconds (time);
		collision.gameObject.SetActive (false);
		isMining = false;
		soundManager.StopLoop ();
	}

	IEnumerator WaitForTime (float time)
	{
		yield return new WaitForSeconds (time);
	}
		
}
