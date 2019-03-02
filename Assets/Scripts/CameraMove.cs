using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

	public GameObject Player;
	public float speed = 1;
	private Vector3 playerPos;

	

	// Use this for initialization
	void Start () {
		
	}
	
	
	// Update is called once per frame
	void Update () {
		if(Player != null){
			playerPos = Player.transform.position;
			transform.position = Vector3.Slerp(transform.position, playerPos, speed*Time.deltaTime);
		}
		
	}
}
