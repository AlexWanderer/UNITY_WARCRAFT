using UnityEngine;
using System.Collections;

public class SpawnHuman : MonoBehaviour {

	private float spawningTimer = 0.0f;
	private float spawnMax = 10.0f;

	public humanManage omanage;
	public mainCharacter3 human;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawningTimer += Time.deltaTime;
		if (spawningTimer >= spawnMax)
		{
			Debug.Log("New Human unit is ready !");
			spawningTimer = 0.0f;
			mainCharacter3 newOne = Instantiate(human);
			newOne.transform.position = new Vector2(6.7f, -1.5f);
			omanage.heroes.Add(newOne);
		}
	}
}
