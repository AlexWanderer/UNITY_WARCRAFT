using UnityEngine;
using System.Collections;

public class SpawnOrc4 : MonoBehaviour {

	private float spawningTimer = 0.0f;
	public float spawnMax = 5.0f;

	public orcManage4 omanage;
	public mainCharacter4 orc;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		spawningTimer += Time.deltaTime;
		if (spawningTimer >= spawnMax)
		{
			Debug.Log("New Orc unit is ready !");
			spawningTimer = 0.0f;
			mainCharacter4 newOne = Instantiate(orc);
			newOne.transform.position = new Vector2(-3.7f, 1.7f);
			omanage.heroes.Add(newOne);
		}
	}
}
