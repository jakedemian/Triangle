using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour {
	public float jumpSpeed = 5f;

	Rigidbody rb;
	bool grounded;
	void Start(){
		rb = GetComponent<Rigidbody>();

		grounded = true;
		clampToGround();
	}

	void Update () {
		if(Input.GetButtonDown("Jump") && grounded){
			grounded = false;
			rb.velocity += Vector3.up * jumpSpeed;
		}

		if(isOnOrBelowGround()){
			grounded = true;
		}

		// if(grounded){
		// 	clampToGround();
		// }
	}

	bool isOnOrBelowGround(){
		RaycastHit hit;
		bool isOnOrBelowGround = false;
		if(Physics.Raycast(transform.position, -Vector3.up, out hit)){
			float distanceToGround = hit.distance;
			if(distanceToGround <= GetComponent<Collider>().bounds.extents.y){
				isOnOrBelowGround = true;
			}
		}

		return isOnOrBelowGround;
	}

	void clampToGround(){
		RaycastHit hit;
		if(Physics.Raycast(transform.position, -Vector3.up, out hit)){
			float distanceToGround = hit.distance;
			transform.position = new Vector3(transform.position.x, transform.position.y - distanceToGround + GetComponent<Collider>().bounds.extents.y , transform.position.z);
		}
	}
}
