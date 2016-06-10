using UnityEngine;
using System.Collections;

public class Spikes : MonoBehaviour {

	private Player player;
	void Start() {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.CompareTag ("Player")) {
			player.Damage (3);
			StartCoroutine (player.KnockBack(0.02f,350,player.transform.position));
		}
	}
}
