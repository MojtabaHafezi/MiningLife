using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	//Components
	private Rigidbody2D rigidBody;
	private Animator animator;

	//Attributes
	public float horizontalSpeed;
	public float verticalSpeed;
	private float currentSpeed = 3;
	private float currentSpeedNegative = -3;
	private bool facingRight = true;
	private bool isMining = false;

	//debugging
	public float horizontal;
	public float vertical;

	void Start ()
	{
		rigidBody = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		facingRight = true;
	}

	//fixedupdate is called just before performing physic calculations -> movement comes here (no Time.deltatime)
	void FixedUpdate ()
	{
		float newSpeed;

		//Keys are set in the InputManager by default
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//debugging
		horizontal = moveHorizontal;
		vertical = moveVertical;

		//set speed for walking animation - mathf.abs for positive value only
		animator.SetFloat ("speed", Mathf.Abs (moveHorizontal));

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

		//Keys are set in the InputManager by default
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		//debugging
		horizontal = moveHorizontal;
		vertical = moveVertical;


		Vector3 pos = this.gameObject.transform.position;
		Vector3 comparePos = collision.transform.position;

		if (vertical != 0)
			horizontal = 0;
		if (horizontal != 0)
			vertical = 0;
		if (!isMining) {
			
		
			if (collision.gameObject.tag == "Resource") {
				//check if mineable 


				if (vertical < 0) {
					if (comparePos.y < pos.y && Mathf.Abs (comparePos.x - pos.x) < 0.2) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}
				
				} else if (horizontal < 0) {
					if (comparePos.x < pos.x && Mathf.Abs (comparePos.y - pos.y) < 0.2) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}
				
				} else if (horizontal > 0) {
					if (comparePos.x > pos.x && Mathf.Abs (comparePos.y - pos.y) < 0.2) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}
				}

			} 

			if (collision.gameObject.tag == "Tile") {
				//what to do if the object is a tile
				if (vertical < 0) {
					if (comparePos.y < pos.y && Mathf.Abs (comparePos.x - pos.x) < 0.2) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}

				} else if (horizontal < 0) {
					if (comparePos.x < pos.x && Mathf.Abs (comparePos.y - pos.y) < 0.2) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}

				} else if (horizontal > 0) {
					if (comparePos.x > pos.x && Mathf.Abs (comparePos.y - pos.y) < 0.2) {
						StartCoroutine (WaitForTime (collision, 1.0f));
					}
				}
			}
		}
	}

	IEnumerator WaitForTime (Collision2D collision, float time)
	{
		//animation etc.
		isMining = true;
		yield return new WaitForSeconds (time);
		collision.gameObject.SetActive (false);
		isMining = false;
	}

	/*
	 * if (vertical < 0 || horizontal < 0 || horizontal > 0) {


				StartCoroutine (WaitForTime (collision, 1.0f));

			}
			*/


}
