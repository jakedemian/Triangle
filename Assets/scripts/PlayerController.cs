using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	Camera cam;
	Rigidbody rb;

	const float PLAYER_RAW_FORWARD_MOVE_SPEED = 20f;
	const float PLAYER_RAW_FORWARD_SPRINT_SPEED = 30f;
	const float PLAYER_RAW_BACKWARD_MOVE_SPEED = 7f;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 camForward = cam.transform.forward.normalized;
		Vector3 camRight = cam.transform.right.normalized;

		Vector3 verticalVector = Vector3.zero;
		Vector3 horizontalVector = Vector3.zero;
		Vector3 movementVector = Vector3.zero;

		if(Input.GetAxisRaw("Vertical") == 1){
			verticalVector = new Vector3(camForward.x, 0, camForward.z);
		} else if(Input.GetAxisRaw("Vertical") == -1){
			verticalVector = new Vector3(camForward.x, 0, camForward.z) * -1;
		}

		if(Input.GetAxisRaw("Horizontal") == 1){
			horizontalVector = new Vector3(camRight.x, 0, camRight.z);
		} else if(Input.GetAxisRaw("Horizontal") == -1){
			horizontalVector = new Vector3(camRight.x, 0, camRight.z) * -1;
		}

		if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") != 0){
			horizontalVector /= Mathf.Sqrt(2f);
			verticalVector /= Mathf.Sqrt(2f);
		}

		movementVector = horizontalVector + verticalVector;
		float currentMoveSpeed = 0f;
		if(Input.GetAxisRaw("Vertical") == -1){
			currentMoveSpeed = PLAYER_RAW_BACKWARD_MOVE_SPEED;
		} else {
			if(Input.GetButton("Sprint")){
				currentMoveSpeed = PLAYER_RAW_FORWARD_SPRINT_SPEED;
			} else {
				currentMoveSpeed = PLAYER_RAW_FORWARD_MOVE_SPEED;
			}
		}

		transform.Translate(movementVector * Time.deltaTime * currentMoveSpeed);

		//rotate the player between these
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	}
}
