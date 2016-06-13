using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour {

	public float maxSpeed = 3;
	public float speed = 50f;
	public float jumpPower = 150f;

	public bool grounded;
	public bool canDoubleJump;

	public int curHealth;
	public int maxHealth = 5;

	private Rigidbody2D rb2d;
	private Animator anim;
	// Use this for initialization
	void Start () {
		rb2d = gameObject.GetComponent<Rigidbody2D> ();
		anim = gameObject.GetComponent<Animator> (); 

		curHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetBool ("Grounded", grounded);
		anim.SetFloat ("Speed", Mathf.Abs (rb2d.velocity.x ));
		if (Input.GetAxis ("Horizontal") < -0.1f) {
			transform.localScale = new Vector3 (-1, 1, 1);
		}
		if (Input.GetAxis ("Horizontal") > 0.1f) {
			transform.localScale = new Vector3 (1, 1, 1);
		}
		if (Input.GetButtonDown ("Jump")) {
			if (grounded) {
				rb2d.AddForce (Vector2.up * jumpPower);
				canDoubleJump = true;
			} else {
				if (canDoubleJump) {
					canDoubleJump = false;
					rb2d.velocity = new Vector2 (rb2d.velocity.x, 0);
					rb2d.AddForce (Vector2.up * jumpPower /1.75f);
				}
			}
		}
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curHealth <= 0) {
			Die ();
		}
	}

	void FixedUpdate () {
		Vector3 easeVelocity = rb2d.velocity; 
		easeVelocity.y = rb2d.velocity.y;
		easeVelocity.z = 0.0f;
		easeVelocity.x *= 0.75f;

		float h = Input.GetAxis ("Horizontal");

		// Fake friction /  Easing the x speed of our player
		if (grounded) {
			rb2d.velocity = easeVelocity;
		}

		rb2d.AddForce ((Vector2.right * speed) * h); 
		if (rb2d.velocity.x > maxSpeed) {
			rb2d.velocity = new Vector2 (maxSpeed, rb2d.velocity.y);
		} 
		if (rb2d.velocity.x < -maxSpeed) {
			rb2d.velocity = new Vector2 (-maxSpeed, rb2d.velocity.y);
		}
	}

	void Die() {
		// Restart
//		Application.LoadLevel(Application.loadedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void Damage (int dmg) {
		if (curHealth <= dmg) {
			Die ();
		} else {
			curHealth -= dmg;
		}
		gameObject.GetComponent<Animation> ().Play ("Player_RedFlash");
	}

	public IEnumerator KnockBack(float knockDur, float knockbackPwr, Vector3 knockbackDir) {
		float timer = 0;
		while (knockDur > timer) {
			timer += Time.deltaTime;
			rb2d.AddForce (new Vector3(knockbackDir.x * -100, knockbackDir.y *knockbackPwr, transform.position.z));
		}

		yield return 0;
	}
}
