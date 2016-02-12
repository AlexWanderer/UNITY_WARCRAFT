using UnityEngine;
using System.Collections;

public class gamerule : MonoBehaviour {

	public pvBuildings4 buildOrc;
	public pvBuildings4 buildHuman;

	void OnEnable() {
		buildOrc.Ondead += EndGame;
		buildHuman.Ondead += EndGame;
	}

	void OnDisable() {
		buildOrc.Ondead += EndGame;
		buildHuman.Ondead += EndGame;
	}
	
	void EndGame()
	{
		if (buildOrc.pv <= 0){
			Debug.Log("FIN DU GAME !! || HUMAN WINS");
			Application.LoadLevel("humansWin");
		} else if (buildHuman.pv <= 0){
			Debug.Log("FIN DU GAME !! || ORC WINS");
			Application.LoadLevel("orcsWin");
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}
}
