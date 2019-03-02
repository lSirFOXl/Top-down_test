using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLineController : MonoBehaviour {

	private List<GameObject> heartList = new List<GameObject>();

	[SerializeField]
	private GameObject heartPref;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void changeHeart(int hp){
		int forI = heartList.Count;
		for (int i = 0; i < forI; i++)
		{
			Destroy(heartList[0]);
			heartList.RemoveAt(0);
		}

		for (int i = 0; i < hp; i++)
		{
			GameObject hpV = Instantiate(heartPref, new Vector3(0,0,0), Quaternion.identity);
			hpV.transform.SetParent(transform);

			RectTransform hpRT = hpV.GetComponent<RectTransform>();
			hpRT.anchoredPosition = new Vector2(0+20*i, 0);
			hpRT.transform.localScale = new Vector3(1f,1f,1f);

			heartList.Add(hpV);
		}
	}
}
