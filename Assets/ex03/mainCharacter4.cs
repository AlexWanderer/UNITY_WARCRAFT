using UnityEngine;
using System.Collections;

public class mainCharacter4 : MonoBehaviour {

//	public float	speed = 0.0f;
//	public float	maxSpeed = 2.0f;

	public	Vector2	direction;
	private float	angle = 0.0f;

	public AudioSource 	words1;
	public AudioSource 	words2;
	public AudioSource 	words3;

	Animator anim;

	public mainCharacter4	humanToAttack;
	public pvBuildings4		buildToAttack;
	public bool				goal = false;

	public orcManage4 	orcMNG;
	public humanManage4	humanMNG;

	public int			pv = 20;
	public int			damage = 5;

	public float		timerDamge = 0.0f;
	public float		timerDamageMax = 1.0f;

	private Rigidbody2D rb;
	
	void OnMouseDown() {
		if (this.tag == "human" && !orcMNG.selecting)
		{
			foreach (mainCharacter4 orc in orcMNG.selected)
			{
				orc.buildToAttack = null;
				orc.humanToAttack = this;
			}
		}
	}

	void OnCollisionStay2D(Collision2D coll) {
		if (this.buildToAttack != null && coll.gameObject.name == buildToAttack.gameObject.name)
			this.goal = true;
		else if (humanToAttack != null && coll.gameObject.name == humanToAttack.gameObject.name)
			this.goal = true;
		else
			this.goal = false;
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = this.GetComponent<Rigidbody2D> ();
		direction = rb.position;
	}

	// Update is called once per frame
	void Update () {

		timerDamge += Time.deltaTime;
		if (timerDamge >= timerDamageMax)
		{
			if (goal && humanToAttack != null) {
				humanToAttack.takeDamage(this.damage);
				Debug.Log("Human Unit [" + humanToAttack.pv + "/20] has been attacked");
			} else if (goal && buildToAttack != null) {
				buildToAttack.takeDamage(this.damage);
				Debug.Log("Human Buildiing [" + buildToAttack.pv + "/" + buildToAttack.pvMax + "] has been attacked");
			}
			timerDamge = 0;
		}

		if (humanToAttack == null && buildToAttack == null)
			goal = false;

		if (goal)
			anim.SetBool ("attacking", true);
		else
			anim.SetBool("attacking", false);

		if (this.pv <= 0) {
			this.pv = 0;
			anim.SetBool("dead", true);
		} else {
			anim.SetBool("dead", false);
		}

		// goes to position for goal
		if (this.humanToAttack != null && this.goal == false) {
			setDirection(humanToAttack.transform.position);
		} else if(this.buildToAttack != null && goal == false) {
			setDirection(buildToAttack.transform.position);
		}

		// reset target if dead
		if (humanToAttack != null && humanToAttack.pv <= 0) {
			humanToAttack.GetComponent<BoxCollider2D>().size = Vector2.zero;
			humanToAttack = null;
			goal = false;
		}
		if (buildToAttack != null && buildToAttack.pv <= 0) {
			buildToAttack = null;
			goal = false;
		}

		if(!Mathf.Approximately(rb.position.magnitude, direction.magnitude)) {
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, direction, 1 / (15.0f * (Vector3.Distance (gameObject.transform.position, direction))));
			if (!goal)
				anim.SetBool ("walking", true);
		} else {
			anim.SetBool ("walking", false);
			transform.localScale = new Vector3(1, 1, 1);
		}
	}

	public void setDirection(Vector2 dir){

		Vector2 origin = new Vector2 (0.0f, 0.0f);
		if (rb.position != dir) {
			if (dir == origin)
				direction = new Vector2(0.1f, 0.1f);
			else
				direction = dir;

			getAngle();

			// determine state
			if (angle >= -22.5 && angle <= 22.5)
				anim.SetInteger("state", 0); //up
			else if ((angle >= -112.5 && angle <= -67.5) || (angle >= 67.5 && angle <= 112.5))
				anim.SetInteger("state", 1); //side
			else if ((angle <= (-180 + 22.5) && angle >= -180) || (angle <= 180 && angle >= (180 - 22.5)))
				anim.SetInteger("state", 2); //down
			else if ((angle >= -45f - 22.5f && angle <= -45f + 22.5f) || (angle >= 22.5f && angle <= 67.5))
				anim.SetInteger("state", 3); //diag up
			else
				anim.SetInteger("state", 4); //diag down
			Flip ();
		}
	}

	private void Flip()
	{
		if (anim.GetInteger ("state") == 1 && (angle >= -112.5 && angle <= -67.5))
			transform.localScale = new Vector3(-1, 1, 1);
		else if (anim.GetInteger ("state") == 1)
			transform.localScale = new Vector3(1, 1, 1);
		else if (anim.GetInteger ("state") == 3 && (angle >= 22.5f && angle <= 67.5))
			transform.localScale = new Vector3(1, 1, 1);
		else if (anim.GetInteger ("state") == 4 && (angle >= 135f - 22.5f && angle <= 135f + 22.5f))
			transform.localScale = new Vector3(1, 1, 1);
		else if (anim.GetInteger ("state") == 4 && (angle >= -135f - 22.5f && angle <= -135f + 22.5f))
			transform.localScale = new Vector3(-1, 1, 1);
		else if (anim.GetInteger ("state") == 3 && (angle >= -45f - 22.5f && angle <= -45f + 22.5f))
			transform.localScale = new Vector3(-1, 1, 1);
		else
			transform.localScale = new Vector3(1, 1, 1);
	}

	private void getAngle() {
		angle = Mathf.Atan2 (direction.x - rb.position.x, direction.y - rb.position.y);
		angle = angle * 180 / Mathf.PI;
	}

	public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
	{
		Vector3 P = x * Vector3.Normalize(B - A) + A;
		return P;
	}

	public void	takeDamage(int dam)
	{
		pv -= dam;
	}
}
