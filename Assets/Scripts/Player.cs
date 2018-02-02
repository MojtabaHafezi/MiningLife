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




}
