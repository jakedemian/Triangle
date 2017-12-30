using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	private Camera cam;

	private const float TEMP__PLAYER_RAW_FORWARD_MOVE_SPEED = 10f;
	private const float TEMP__PLAYER_RAW_BACKWARD_MOVE_SPEED = 5f;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
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
		transform.Translate(movementVector * Time.deltaTime * TEMP__PLAYER_RAW_FORWARD_MOVE_SPEED);
	}
}
