using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GlobalVars : MonoBehaviour {

	public GameController gameControllerGV;
	public static GameController gameController;	

	public HeartLineController hpLineGV;
	public static HeartLineController hpLine;

	public GameObject dynamicSceneGV;
	public static GameObject dynamicScene;

	public GameObject enemyPrefGV;
	public static GameObject enemyPref;


	void Awake()
	{	
		gameController = gameControllerGV;
		hpLine = hpLineGV;
		enemyPref = enemyPrefGV;
		dynamicScene = dynamicSceneGV;
	}

	

}
