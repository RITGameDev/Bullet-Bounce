using UnityEngine;
using System.Collections;

public class ControlGun : MonoBehaviour {

	public KeyCode lRot, shoot, rRot;
	public GameObject bullet;
	Vector3 spinVect = new Vector3(0, 0, 100);

	void Update () {
		Movement();
	}

	/* Shoots a bullet. */
	void Shoot() {
		Instantiate(bullet, new Vector2(transform.position.x, transform.position.y), transform.rotation);
	}

	/* Rotates the gun either left or right; also handles shooting. */
	void Movement() {
		if (Input.GetKey(lRot)) {
			transform.Rotate(spinVect * Time.deltaTime); }
		else if (Input.GetKey(rRot)) {
			transform.Rotate(-spinVect * Time.deltaTime); }
		if (Input.GetKeyDown(shoot)) {
			Shoot(); }
	}
}