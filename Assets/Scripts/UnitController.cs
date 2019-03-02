using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitController : MonoBehaviour {

	public GameObject bullet;

	public int maxHP;
	private int currentHP;
	public bool isMain = false;
	public bool isDead = false;
	

	// Use this for initialization
	void Start () {
		
		currentHP = maxHP;
		if(isMain) GlobalVars.hpLine.changeHeart(currentHP);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void shoot(){
		GameObject bulletV = Instantiate (Resources.Load ("Bullet") as GameObject);
		BulletController bulletCV = bulletV.GetComponent<BulletController>();
		bulletCV.owner = gameObject;
		bulletV.transform.position = transform.position;
		bulletV.transform.rotation = transform.rotation;
	}

	public void takeDamage(GameObject fromUnit){
		
		if(isMain && GlobalVars.gameController.godmode) {} else currentHP--;

		if(isMain){
			GlobalVars.hpLine.changeHeart(currentHP);
		}

		if(currentHP <= 0) {
			if(fromUnit == GlobalVars.gameController.unitList[0].gameObject)  GlobalVars.gameController.heroUnitKillUnit();
			dead();
		}
	}

	private void dead(){
		isDead = true;
		GlobalVars.gameController.unitDied(this);
		if(isMain) GlobalVars.gameController.mainUnitDied();
		Destroy(gameObject);
	}

	
}
