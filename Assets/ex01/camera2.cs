using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class camera2 : MonoBehaviour {
	
	public bool					selecting = false;
	public bool					moving = false;
	private Camera				cam;

	public Vector3				initMousePos;
	public Vector3				act;

	public List<mainCharacter2> heroes = new List<mainCharacter2>();
	public List<mainCharacter2> selected = new List<mainCharacter2>();

	private float				margin = 0.5f;

	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {

		showSelected();

		if (Input.GetKey(KeyCode.LeftControl))
			selecting = true;

		if (Input.GetKeyUp(KeyCode.LeftControl))
			selecting = false;

		if (Input.GetMouseButtonDown(1))
			selected.Clear();

		if (Input.GetMouseButtonUp(0))
		{
			// select all heroes inside mousepos
			moving = false;
			if (!selecting) {
				foreach (mainCharacter2 hero in selected)
				{
					Vector3 newDir = cam.ScreenToWorldPoint(Input.mousePosition);
					hero.setDirection(newDir);
				}
			} else {
				foreach (mainCharacter2 hero in heroes)
				{
					Vector2 iniMousep = cam.ScreenToWorldPoint(initMousePos);
					Vector2 actMousep = cam.ScreenToWorldPoint(act);

					// DEBUG INFORMATION
//					Debug.DrawRay(iniMousep, new Vector3(2, 2, 0), Color.green);
//					Debug.DrawRay(actMousep, new Vector3(2, 2, 0), Color.red);
					
					bool isInsideX = (Mathf.Clamp(hero.transform.localPosition.x, iniMousep.x, actMousep.x) != iniMousep.x && Mathf.Clamp(hero.transform.localPosition.x, iniMousep.x, actMousep.x) != actMousep.x);
					bool isInsideY = (Mathf.Clamp(hero.transform.localPosition.y, iniMousep.y, actMousep.y) == iniMousep.y && Mathf.Clamp(hero.transform.localPosition.y, iniMousep.y, actMousep.y) != actMousep.y);
					
					if (isInsideX && isInsideY && !isInSelectedList(hero))
						selected.Add(hero);
				}
			}
			
			// reset parameters
			initMousePos = new Vector2(0, 0);
			act = new Vector2(0, 0);
			selecting = false;
		}

		if (Input.GetMouseButton(0)) {

			Vector2 mp = cam.ScreenToWorldPoint(Input.mousePosition);

			if (!selecting)
			{
				foreach (mainCharacter2 hero in heroes)
				{
					Vector2 realPos = hero.transform.position;

					if (Mathf.Abs(realPos.x - mp.x) <= margin && Mathf.Abs(realPos.y - mp.y) <= margin) {
						selected.Clear();
						selected.Add(hero);
					}
				}
			}
			if (moving == false) {
				initMousePos = (Input.mousePosition);
				moving = true;
			}else{
				act = (Input.mousePosition);

				//DEBUG INFORMATION
//				Vector2 iniMousep = cam.ScreenToWorldPoint(initMousePos);
//				Vector2 actMousep = cam.ScreenToWorldPoint(act);
//
//				Debug.DrawRay(iniMousep, new Vector3(1, 1, 0), Color.green);
//				Debug.DrawRay(actMousep, new Vector3(1, 1, 0), Color.red);
			}
		}
	}

	private void showSelected()
	{
		foreach (mainCharacter2 hero in heroes)
		{
			if (selected.Contains(hero))
				hero.GetComponent<SpriteRenderer>().color = Color.green;
			else
				hero.GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	private bool isInSelectedList(mainCharacter2 currentHero) {
		bool isSelected = false;
		foreach (mainCharacter2 hero in selected)
		{
			if (hero == currentHero)
				isSelected = true;
		}
		return isSelected;
	}

	void OnGUI () {
		if (selecting && act.x != 0 && act.y != 0)
			GUI.Box(new Rect(initMousePos.x, Screen.height - initMousePos.y, act.x - initMousePos.x, (initMousePos.y - act.y)), " ");
	}

}
