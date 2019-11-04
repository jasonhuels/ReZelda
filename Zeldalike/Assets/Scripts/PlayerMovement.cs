﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
	walk,
	attack,
	interact
}

public class PlayerMovement : MonoBehaviour {
	public PlayerState currentState;
	public float speed;
	private Rigidbody2D myRigidbody;
	private Vector3 change;
	private Animator animator;
    //float MaxSpeed = 10;//This is the maximum speed that the object will achieve
    //float Acceleration = 10;//How fast will object reach a maximum speed
	//float Deceleration = 10;//How fast will object reach a speed of 0
	public Collider box = GameObject.Find("box").GetComponent<Collider>();
    public Collider player = GameObject.Find("player").GetComponent<Collider>();

	// Use this for initialization
	void Start () {
		currentState = PlayerState.walk;
		animator = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		change = Vector3.zero;
		change.x = Input.GetAxis("Horizontal");
		change.y = Input.GetAxis("Vertical");
		if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
		{
			StartCoroutine(AttackCo());
		}
		else if(currentState == PlayerState.walk)
		{
			UpdateAnimationAndMove();
		}
		if(player.bounds.Intersects(box.bounds) && Input.GetKeyDown("shift"))
		{
            Debug.Log("grabbed ");
            box.transform.SetParent(player.transform);
		}
	}

	private IEnumerator AttackCo()
	{
		animator.SetBool("attacking", true);
		currentState = PlayerState.attack;
		yield return null;
		animator.SetBool("attacking", false);
		yield return new WaitForSeconds(0.3f);
		currentState = PlayerState.walk;
	}

	void UpdateAnimationAndMove()
	{
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
	}

	void MoveCharacter()
	{
		myRigidbody.MovePosition(transform.position + change * speed * Time.deltaTime);
	}
}
