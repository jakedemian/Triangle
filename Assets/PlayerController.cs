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
		float vert = Input.GetAxisRaw("Vertical");
		float horiz = Input.GetAxisRaw("Horizontal");

		if(vert == 1){
			// get camera facing direction
			Vector3 dirRaw = cam.transform.forward.normalized;
			Vector3 forward = new Vector3(dirRaw.x, 0, dirRaw.z); // horrible i'm gunna puke pls fix
			transform.Translate(forward * Time.deltaTime * TEMP__PLAYER_RAW_FORWARD_MOVE_SPEED);
		} else if(vert == -1){
			Vector3 dirRaw = cam.transform.forward.normalized;
			Vector3 backward = new Vector3(dirRaw.x, 0, dirRaw.z) * -1; // horrible i'm gunna puke pls fix
			transform.Translate(backward * Time.deltaTime * TEMP__PLAYER_RAW_BACKWARD_MOVE_SPEED);
		}

		if(horiz == 1){
			Vector3 dirRaw = cam.transform.right.normalized;
			Vector3 right = new Vector3(dirRaw.x, 0, dirRaw.z); // horrible i'm gunna puke pls fix
			transform.Translate(right * Time.deltaTime * TEMP__PLAYER_RAW_FORWARD_MOVE_SPEED);
		} else if(horiz == -1){
			Vector3 dirRaw = cam.transform.right.normalized;
			Vector3 left = new Vector3(dirRaw.x, 0, dirRaw.z) * -1; // horrible i'm gunna puke pls fix
			transform.Translate(left * Time.deltaTime * TEMP__PLAYER_RAW_FORWARD_MOVE_SPEED);
		}

		Debug.Log("don't forget to do you TODO you stupid idiot but i'm tired so i'm calling it quits for tonight <3");

		// TODO i need to refactor this a bit.  firstly, i'm repeating the same thing 4 times so it should probly be a methor or something.
		// also i have to fix the diagonal bug, so i need to calculate the values but NOT instantly Translate.  get both the horizontal and the vertical
		// axis calculation complete, save them, then at the end of Update() do the Translate.  if horizontal and vertical movements are present, I need
		// to clamp the values by dividing (maybe multiplying, idr do the math you moron) by Mathf.Sqrt(2)
	}
}
