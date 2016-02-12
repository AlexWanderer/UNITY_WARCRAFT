using UnityEngine;
using System.Collections;

public class pvBuildings4 : MonoBehaviour {

	public delegate	void 		buildEvent ();
	public event buildEvent		Ondead;

	public orcManage4 	orcMNG;
	public humanManage4	humanMNG;
	
	public int pv = 0;
	public int pvMax;

	// Use this for initialization
	void Start () {
		pv = pvMax;
	}

	void OnMouseDown() {
		if (!orcMNG.selecting)
		{
			foreach (mainCharacter4 orc in orcMNG.selected)
			{
				orc.humanToAttack = null;
				orc.buildToAttack = this;
			}
		}
	}

	// Update is called once per frame
	void Update () {

		if (pv <= 0) {
			if (Ondead != null)
				Ondead();
		}
	}

	public void takeDamage(int dmg)
	{
		this.pv -= dmg;
	}
}
