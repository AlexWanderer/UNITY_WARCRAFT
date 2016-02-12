using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class camera4 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hit)
		{
		}
	}
}