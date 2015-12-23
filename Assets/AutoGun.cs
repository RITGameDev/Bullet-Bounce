using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoGun : MonoBehaviour {

	public GameObject bullet;
	Vector3 spinVect = new Vector3(0, 0, 27.5f);

	void Awake() {
		InvokeRepeating("Shoot", 1f, 1f);
	}

	/* Rotates the gun around the player. */
	void Update () {
		transform.Rotate(spinVect * Time.deltaTime);
	}

	/* Shoots according to the difficulty. */
	void Shoot() {
		Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), transform.rotation);
	}
}
