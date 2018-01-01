using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour {

	public float downAccel = 7.75f;
	public float jumpVel = 3;
	public float distToGround = 0.5f;
	public LayerMask groundMask;
	public string JUMP_AXIS = "Jump";
	Rigidbody rb;

	void Start(){
		rb = GetComponent<Rigidbody>();
	}

	bool Grounded(){
		bool res = Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y, groundMask);
		if(res){
			rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
		}
		return res;
	}

	void Update () {
		if(Input.GetAxisRaw(JUMP_AXIS) != 0 && Grounded()){
			Jump();
		}

		rb.AddForce(-Vector3.up * downAccel);
	}

	void Jump(){
		rb.velocity = new Vector3(rb.velocity.x, jumpVel, rb.velocity.z);
	}
}
