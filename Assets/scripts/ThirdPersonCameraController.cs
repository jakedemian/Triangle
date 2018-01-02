using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour {
	public bool lockCursor;
	public float mouseSensitivity = 10;
	public Transform target;
	public float distFromTarget = 2;
	public bool invertPitch = false;
	float yaw;
	float pitch;
	public Vector2 pitchMinMax = new Vector2(-40,85);
	public Vector2 camDistMinMax = new Vector2(2, 5);

	public float rotationSmoothTime = 0.12f;
	float zoomSpeed = 700f;
	Vector3 rotationSmoothVelocity;
	Vector3 currentRotation;

	float currentZoom;
	float zoomSmoothTime = 0.2f;
	float zoomSmoothVelocity;

	void Start(){
		if(lockCursor){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
	}	
	
	void LateUpdate () {
		// obtain target pitch and yaw
		yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
		if(invertPitch){
			pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
		} else {
			pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		}
		pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

		// calculate target zoom distance
		distFromTarget -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * Time.deltaTime;
		distFromTarget = Mathf.Clamp(distFromTarget, camDistMinMax.x, camDistMinMax.y);

		currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch,yaw), ref rotationSmoothVelocity, rotationSmoothTime);
		currentZoom = Mathf.SmoothDamp(currentZoom, distFromTarget, ref zoomSmoothVelocity, zoomSmoothTime);

		transform.eulerAngles = currentRotation;
		transform.position = target.position - transform.forward * currentZoom;
	}
}
