﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
	private Animator animator;
	private bool slide;

	// Use this for initialization
	void Awake ()
	{
		animator = GetComponent<Animator> ();
		slide = false;
		animator.SetBool (CONSTANTS.SLIDE, slide);
	}

	public void SetSlideParameter ()
	{
		slide = !slide;
		animator.SetBool (CONSTANTS.SLIDE, slide);
	}
}
