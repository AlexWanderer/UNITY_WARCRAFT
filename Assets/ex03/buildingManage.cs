using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class buildingManage : MonoBehaviour {

	public List<pvBuildings4> humanBuildings;
	public List<pvBuildings4> orcBuildings;

	public SpawnHuman4			spawnHuman;
	public SpawnOrc4			spawnOrc;

	public AudioSource			orcDestroy;
	public AudioSource			humanDestroy;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		int sizeOHuman = humanBuildings.Count;
		int sizeOOrc = orcBuildings.Count;

		foreach (pvBuildings4 build in humanBuildings.ToList())
		{
			if (build.pv <= 0)
			{
				humanDestroy.Play();
				build.GetComponent<PolygonCollider2D>().enabled = false;
				build.GetComponent<SpriteRenderer>().enabled = false;
				humanBuildings.Remove(build);
				//Destroy(build.gameObject);
			}
		}
		foreach (pvBuildings4 build in orcBuildings.ToList())
		{
			if (build.pv <= 0)
			{
				orcDestroy.Play();
				build.GetComponent<PolygonCollider2D>().enabled = false;
				build.GetComponent<SpriteRenderer>().enabled = false;
				orcBuildings.Remove(build);
				//Destroy(build.gameObject);
			}
		}

		spawnHuman.spawnMax = 10 + (5 - sizeOHuman) * 2.5f;
		spawnOrc.spawnMax = 10 + (5 - sizeOOrc) * 2.5f;
	}
}
