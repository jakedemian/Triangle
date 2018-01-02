﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	public float walkSpeed = 2;
	public float runSpeed = 6;
	public float turnSmoothTime = 0.2f;
	public float speedSmoothTime = 0.1f;
	public float jumpHeight = 1;
	[Range(0,1)]
	public float airControlPercent;

	float turnSmoothVelocity;
	float speedSmoothVelocity;
	float currentSpeed;
	Animator animator;
	public float gravity = -12f;
	float velocityY; // current y velocity

	Transform cameraT;
	CharacterController controller;

	void Start () {
		animator = GetComponent<Animator>();
		cameraT = Camera.main.transform;
		controller = GetComponent<CharacterController>();
	}
	
	void Update () {
		//input 
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;
		bool running = Input.GetKey(KeyCode.LeftShift);

		Move(inputDir, running);
		if(Input.GetKeyDown(KeyCode.Space)){
			Jump();
		}

		// animation control
		float animationSpeedPercent = ((running) ? currentSpeed/runSpeed : currentSpeed/walkSpeed * 0.5f);
		animator.SetFloat("speedPercent", animationSpeedPercent, speedSmoothTime, Time.deltaTime);
	}

	void Move(Vector2 inputDir, bool running){
		if(inputDir != Vector2.zero){
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, GetModifiedSmoothTime(turnSmoothTime));
		}

		float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
		currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, GetModifiedSmoothTime(speedSmoothTime));

		velocityY += Time.deltaTime * gravity;

		Vector3 velocity = (transform.forward * currentSpeed) + (Vector3.up * velocityY);
		controller.Move(velocity * Time.deltaTime);
		currentSpeed = new Vector2(controller.velocity.x, controller.velocity.z).magnitude;

		if(controller.isGrounded){
			velocityY = 0;
		}

		
	}

	void Jump(){
		if(controller.isGrounded){
			float jumpVelocity = Mathf.Sqrt(-2*gravity*jumpHeight);
			velocityY = jumpVelocity;
		}
	}

	float GetModifiedSmoothTime(float smoothTime){
		if(controller.isGrounded){
			return smoothTime;
		}
		if(airControlPercent == 0){
			return float.MaxValue;
		}
		return smoothTime / airControlPercent;
	}
}
 