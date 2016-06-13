using UnityEngine;
using System.Collections;

public class TurretIA : MonoBehaviour {

	//Intergers
	public int curHealth;
	public int maxHealth;

	//Floats
	public float distance;
	public float wakeRange;
	public float shootInterval;
	public float bulletSpeed = 100;
	public float bulletTimer;

	//Booleans
	public bool awake = false;
	public bool lookingRight = true;

	//References
	public GameObject bullet;
	public Transform target;
	public Animator anim;
	public Transform shootPointLeft;
	public Transform shootPointRight;

	void Awake() {
		anim = gameObject.GetComponent<Animator> ();
	}

	void Start() {
		curHealth = maxHealth;
	}

	void Update() {
		anim.SetBool ("Awake", awake);
		anim.SetBool ("LookingRight", lookingRight);
		RangeCheck ();

		if (target.transform.position.x > transform.position.x) {
			lookingRight = true;
		}
	}

	void RangeCheck() {
		distance = Vector3.Distance (transform.position, target.transform.position);
		if (distance < wakeRange) {
			awake = true;
		}
		if (distance > wakeRange) {
			awake = false;
		}
	}
}
