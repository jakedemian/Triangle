using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour {
	public float jumpSpeed = 5f;
	Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody>();
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			rb.velocity += Vector3.up * jumpSpeed;
		}
	}
}
