using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	private GameObject player;
	public bool invertHorizontal;
	public bool invertVertical;
	public float orbitSpeedFactor = 100f;

	private Vector3 lastMousePosition;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");


	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform);
		if(Input.GetMouseButton(1)){
			if(Input.GetAxis("Mouse X") != 0){
				float orbitSpeed = orbitSpeedFactor * Input.GetAxis("Mouse X") * Time.deltaTime;
				transform.RotateAround(player.transform.position, Vector3.up, orbitSpeed);
			}
			if(Input.GetAxis("Mouse Y") != 0 && cameraIsNotAtLimits()){
				float orbitSpeed = orbitSpeedFactor * Input.GetAxis("Mouse Y") * Time.deltaTime;
				transform.RotateAround(player.transform.position, Vector3.right, orbitSpeed * -1);

				if(transform.position.y < player.transform.position.y){
					transform.position = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
				}
			}
		}
		lastMousePosition = Input.mousePosition;

		if(Input.GetAxis("Mouse ScrollWheel") != 0){
			GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000f;
		}
	}

	private bool cameraIsNotAtLimits(){
		bool lowerLimitCheck = Input.GetAxis("Mouse Y") < 0 || transform.position.y > player.transform.position.y;
		bool upperLimitCheck = Input.GetAxis("Mouse Y") > 0 || Mathf.Abs(Vector3.Angle(transform.forward, -Vector3.up)) > 10f;
		return lowerLimitCheck && upperLimitCheck;
	}
}
