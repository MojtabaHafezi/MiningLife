using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{

	void OnCollisionEnter2D (Collision2D collision)
	{
		Destroy (collision.gameObject);
	}

	void OnCollisionStay2D (Collision2D collision)
	{
		Destroy (collision.gameObject);
	}
}
