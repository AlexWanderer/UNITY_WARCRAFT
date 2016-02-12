using UnityEngine;
using System.Collections;

public class camera : MonoBehaviour {

	public mainCharacter hero;
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Vector3 newDir = cam.ScreenToWorldPoint(Input.mousePosition);
			hero.setDirection(newDir);
		}
	}
}
