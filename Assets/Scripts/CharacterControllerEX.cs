
using UnityEngine;
using System.Collections;

 
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]
 
public class CharacterControllerEX : MonoBehaviour {
 
	public float speed = 1.5f;
	public float maxVelocityChange = 10.0f;

	public float rotationSpeed = 70;

	private Vector3 direction;
	private float h, v;
	private int layerMask;
	private Rigidbody body;

	private UnitController uc;
	
	void Start () 
	{
		uc = GetComponent<UnitController>();
		body = GetComponent<Rigidbody>();
		body.freezeRotation = true;
		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;
	}

	void FixedUpdate()
	{
		
		Vector3 velocity = body.velocity;
		Vector3 velocityChange = (direction - velocity);
	    velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	    velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	    velocityChange.y = 0;

		body.AddForce(velocityChange, ForceMode.VelocityChange);

	}


	void Update () 
	{
		if(Input.GetMouseButtonDown(0)){
			uc.shoot();
		}

		v = Input.GetAxis("Vertical");
		if(v < 0) v = 0;

		if(Input.GetKey(KeyCode.A)){
			Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, -rotationSpeed, 0) * Time.deltaTime);
			body.MoveRotation(transform.rotation * deltaRotation);
		}
		if(Input.GetKey(KeyCode.D)){
			Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
			body.MoveRotation(transform.rotation * deltaRotation);
		}

		direction = new Vector3(0, 0, v);
		direction = transform.TransformDirection(direction);
		direction = new Vector3(direction.x * speed, 0, direction.z * speed);
	}
}