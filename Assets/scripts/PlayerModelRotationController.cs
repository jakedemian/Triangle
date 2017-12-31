using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelRotationController : MonoBehaviour {
	public GameObject player;
	public float rotationSpeed = 1f;

	private PlayerController pc;

	void Start () {
		pc = player.GetComponent<PlayerController>();
	}
	
	void Update () {
		if(pc.GetLookDirection() != Vector3.zero){
			Vector3 newDir = Vector3.RotateTowards(transform.forward, pc.GetLookDirection(), rotationSpeed * Time.deltaTime, 0.0f);
			transform.rotation = Quaternion.LookRotation(newDir);
		}
	}
}
