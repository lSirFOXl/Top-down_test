using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public List<UnitController> unitList = new List<UnitController>();

	public int maxUnitsInLine = 10;
	public int maxUnits = 50;
	private int currentUnitsCount;

	[SerializeField]
	private Text unitCount;

	public float unitSpawnPadding = 4;
	public float generateRandomRange = 2;

	public GameObject winScreen;
	public GameObject loseScreen;
	public bool godmode = false;

	public Text killCount;
	private int killCountVal = 0;


	// Use this for initialization
	void Start() {
		generateUnits();
		currentUnitsCount = maxUnits;
		showUnitCount();

		//print();
		//Messenger.AddListener("UNIT_DIED", unitDied);
	}

	void OnDestroy() {
		//Messenger.AddListener("UNIT_DIED", unitDied);
	}

	public void unitDied(UnitController unit){
		currentUnitsCount--;
		unitList.Remove(unit);
		if(unitList.Count <= 1 && !unitList[0].isDead && !loseScreen.active) winScreen.SetActive(true);
		//print(currentUnitsCount);
		showUnitCount();

	}

	public void mainUnitDied(){
		loseScreen.SetActive(true);
	}

	public void restartScene(){
		SceneManager.LoadScene("scene");
	}

	private void showUnitCount(){
		unitCount.text = currentUnitsCount+"/"+maxUnits;
	}

	private void generateUnits(){
		int unitCreateCounter = 1;
		//print(Mathf.Ceil((float)maxUnits/(float)maxUnitsInLine));
		
		for (int i = 0; i < Mathf.Ceil((float)maxUnits/maxUnitsInLine); i++)
		{
			for (int j = 0; j < maxUnitsInLine; j++)
			{
				if(unitCreateCounter < maxUnits){
					GameObject unitV = Instantiate (Resources.Load ("UnitEn") as GameObject);
					unitList.Add(unitV.GetComponent<UnitController>());
					unitV.transform.SetParent(GlobalVars.dynamicScene.transform);
					unitV.transform.localPosition = new Vector3(j*unitSpawnPadding-(maxUnitsInLine-1)*unitSpawnPadding/2+Random.Range(-generateRandomRange, generateRandomRange), 4, i*unitSpawnPadding-(Mathf.Ceil((float)maxUnits/maxUnitsInLine)-1)*unitSpawnPadding/2+Random.Range(-generateRandomRange, generateRandomRange));
					unitCreateCounter++;
				}
			}
		}
	}

	public void changeGodmode(Toggle val){
		godmode = val.isOn;
	}

	public void heroUnitKillUnit(){
		killCountVal++;
		killCount.text = killCountVal.ToString();
	}

	
}
