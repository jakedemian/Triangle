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
		if(Input.GetMouseButton(1) && Input.GetAxis("Mouse X") != 0){
			float orbitSpeed = orbitSpeedFactor * Input.GetAxis("Mouse X") * Time.deltaTime;
			transform.RotateAround(player.transform.position, Vector3.up, orbitSpeed);
		}
		lastMousePosition = Input.mousePosition;
	}
}
