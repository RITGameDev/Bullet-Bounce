using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public KeyCode up, down, left, right;
	float speed = .375f;
	public AudioClip lose; 
	AudioSource audio;

	void Awake () {
		audio = GetComponent<AudioSource>();
	}

	void Update () {
		Movement();
	}

	/* Handles directional user input by checking the keycodes. Also handles bounds. */
	void Movement() {
		if (Input.GetKey(up) && transform.position.y < 12) {
			transform.Translate(Vector2.up * speed); }
		else if (Input.GetKey(down) && transform.position.y > -12) {
			transform.Translate(-Vector2.up * speed); }
		if (Input.GetKey(left) && transform.position.x > -32) {
			transform.Translate(-Vector2.right * speed); }
		else if (Input.GetKey(right) && transform.position.x < 32) {
			transform.Translate(Vector2.right * speed); }
	}

	/* Destroys the player; is called after the sound plays. */
	void Destroy() {
		Destroy(this.gameObject);
	}

	/* Checks for collisions with bullets. */
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Bullet") {
			audio.PlayOneShot(lose);
			//Invoke("Destroy", lose.length);
		}
	}
}
