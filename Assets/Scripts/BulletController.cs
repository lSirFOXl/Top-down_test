using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float moveSpeed = 30;
	public float lifetime = 5;
	public GameObject owner;
	private Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody>();
		StartCoroutine(timeToDestroy());
		
	}

	void FixedUpdate () {
		rb.velocity = transform.forward * Time.fixedDeltaTime * moveSpeed;
	}

	IEnumerator timeToDestroy(){
		yield return new WaitForSeconds(lifetime);
		dead();
	} 

	public void dead(){
		GameObject bulletDeleteV = Instantiate (Resources.Load ("BulletDelete") as GameObject);
		bulletDeleteV.transform.position = transform.position;

		Destroy(gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player"){
			if(owner != other.gameObject){
				other.GetComponent<UnitController>().takeDamage(owner);
				dead();
			}
		}
		else if(other.tag == "Wall"){
			dead();
		}
	}
}
