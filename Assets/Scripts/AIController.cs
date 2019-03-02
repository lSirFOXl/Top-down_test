using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour {

	public GameObject target;

	public float maxRotateRaius = 5;
	private float rotateSpeed = 20;
	public Quaternion nextPositionToRotate;
	private Vector3 lastPosition;
	private bool inRotation = false;
	NavMeshAgent aiNav;
       
  void Start () {
		lastPosition = transform.position;
    aiNav = GetComponent<NavMeshAgent>();
		
		aiNav.updateRotation = false;
		
  }
	
	void Update () {
		aiNav.SetDestination(target.transform.position);

		if(aiNav.remainingDistance <= aiNav.stoppingDistance && aiNav.remainingDistance != 0){

			Vector3 targetLookRotation = (target.transform.position - transform.position);
			Quaternion targetRotation = new Quaternion();
			if(targetLookRotation != Vector3.zero){
				targetRotation = Quaternion.LookRotation(targetLookRotation);
			}

			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed/5);
		}
		else{
			Vector3 targetLookRotation = (transform.position - lastPosition);
			Quaternion targetRotation = new Quaternion();
			if(targetLookRotation != Vector3.zero){
				targetRotation = Quaternion.LookRotation(targetLookRotation);
			}

			if(!inRotation) nextPositionToRotate = targetRotation;
			Vector3 targetRotationEu = nextPositionToRotate.eulerAngles;

			if(transform.rotation.eulerAngles.y + maxRotateRaius >= targetRotationEu.y && transform.rotation.eulerAngles.y - maxRotateRaius <= targetRotationEu.y){
				inRotation = false;
				aiNav.updatePosition = true;
				
			}
			else{
				inRotation = true;
				aiNav.updatePosition = false;
			}

			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotationEu.y, 0), Time.deltaTime * rotateSpeed);
			

			lastPosition = transform.position;
		}
		
		
	}
}
