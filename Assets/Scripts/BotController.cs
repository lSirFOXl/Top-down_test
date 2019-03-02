using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotController : MonoBehaviour {

	

	public float maxRotateRaius = 5;
	public float shootSpeed = 1;
	private float rotateSpeed = 20;
	public Quaternion nextPositionToRotate;
	
	private GameObject target;
	private NavMeshAgent aiNav;
	private UnitController uc;
	private Vector3 lastPosition;
	private bool inRotation = false;
	private bool isStay = false;
	private bool shootWaiting = false;
       
	void Start () {
		aiNav = GetComponent<NavMeshAgent>();
		uc = GetComponent<UnitController>();

		lastPosition = transform.position;
		aiNav.updateRotation = false;

		setTarget();
			
	}
	
	void Update () {
		
		if(target != null){
			aiNav.SetDestination(target.transform.position);

			if(aiNav.remainingDistance <= aiNav.stoppingDistance && aiNav.remainingDistance != 0){
				

				Vector3 targetLookRotation = (target.transform.position - transform.position);
				Quaternion targetRotation = new Quaternion();
				if(targetLookRotation != Vector3.zero){
					targetRotation = Quaternion.LookRotation(targetLookRotation);
				}

				transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotateSpeed/5);

				if(!shootWaiting) StartCoroutine(shoot());
				isStay = true;
			}
			else{
				isStay = false;
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

				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, targetRotationEu.y, 0), Time.fixedDeltaTime * rotateSpeed);
				

				lastPosition = transform.position;
			}	
		}
		else{
			setTarget();
		}
		
	}

	private void setTarget(){
		GameObject targetV = null;
		float minDistance = -1;
		foreach (var item in GlobalVars.gameController.unitList)
		{
			if(item != null){
				float distV = Vector3.Distance(item.transform.position, transform.position);
				if((distV < minDistance || minDistance == -1) && item.gameObject != this.gameObject){
					targetV = item.gameObject;
					minDistance = distV;
				}
			}
		}
		target = targetV;
	}

	IEnumerator shoot(){
		shootWaiting = true;
		if(!isStay) uc.shoot();

		yield return new WaitForSeconds(shootSpeed);

		shootWaiting = false;
		if(isStay && target != null){
			uc.shoot();
		}
	} 
}
