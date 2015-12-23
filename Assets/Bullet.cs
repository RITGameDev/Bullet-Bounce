using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	Vector2 moveVect = new Vector2(0, .25f);
	Rigidbody2D rb;
	Collider2D hitbox;
	public AudioClip cancel;
	public AudioClip bounce;
	AudioSource audio;

	/* Gets the components and moves the bullet. */
	void Awake() {
		hitbox = GetComponent<Collider2D>();
		hitbox.enabled = false;
		rb = GetComponent<Rigidbody2D>();
		audio = GetComponent<AudioSource>();
		rb.AddForce(moveVect);
		Invoke("Harm", 0.2f);
	}

	/* Makes the bullet harmful. */
	void Harm() {
		hitbox.enabled = true;
	}
	/* Destroys the bullet; is called after the sound plays. */
	void Destroy() {
		Destroy(this.gameObject);
	}

	/* Handles walls, players, and other bullets. */
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Bullet") {
			// Add X points to the player's score
			rb.velocity = (Vector2.zero);
			audio.PlayOneShot(cancel);
			Invoke("Destroy", cancel.length);
		}
		if (coll.gameObject.tag == "Wall") {
			audio.PlayOneShot(bounce); }
	}
		
}
