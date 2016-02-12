using UnityEngine;
using System.Collections;

public class mainCharacter3 : MonoBehaviour {

//	public float	speed = 0.0f;
//	public float	maxSpeed = 2.0f;

	public	Vector2	direction;
	private float	angle = 0.0f;

	public AudioSource 	words1;
	public AudioSource 	words2;
	public AudioSource 	words3;

	Animator anim;

	public int			pv = 20;
	public int			damage = 5;

	private Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rb = this.GetComponent<Rigidbody2D> ();
		direction = rb.position;
	}

	// Update is called once per frame
	void Update () {
		if(!Mathf.Approximately(rb.position.magnitude, direction.magnitude))
		{
			gameObject.transform.position = Vector3.Lerp (gameObject.transform.position, direction, 1 / (10.0f * (Vector3.Distance (gameObject.transform.position, direction))));
			anim.SetBool ("walking", true);
		} else {
			anim.SetBool ("walking", false);
			transform.localScale = new Vector3(1, 1, 1);
		}
	}

	public void setDirection(Vector2 dir){
		int r = Random.Range(0, 3);
		if (r == 0)
			words1.Play();
		if (r == 1)
			words2.Play();
		if (r == 2)
			words3.Play();
		Vector2 origin = new Vector2 (0.0f, 0.0f);
		if (rb.position != dir) {
			if (dir == origin){
				direction = new Vector2(0.1f, 0.1f);
			}else{
				direction = dir;
			}
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

	public void	takeDamage(int dam)
	{
		pv -= dam;
	}
}
