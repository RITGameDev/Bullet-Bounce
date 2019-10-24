using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutoGun : MonoBehaviour {
	public GameObject bullet;

    int bulletIndex = 0;
    int maxBullets = 50;
	Vector3 spinVect = new Vector3(0, 0, 27.5f);
    Quaternion rotation;

    GameObject[] bullets;

	void Awake() {
        rotation = transform.rotation;

        bullets = new GameObject[maxBullets];
        Vector3 offset = new Vector3(1000, 1000, 0);

        for (int i = 0; i < maxBullets; i++)
            bullets[i] = Instantiate(bullet, offset, transform.rotation) as GameObject;

        InvokeRepeating("Shoot", 2f, 2f);
    }

	/* Rotates the gun around the player. */
	void LateUpdate () {
        transform.rotation = rotation;
        transform.Rotate(spinVect * Time.deltaTime);
        rotation = transform.rotation;
	}

	/* Shoots according to the difficulty. */
	void Shoot() {
        bullets[bulletIndex].transform.GetComponent<Bullet>().Renew(transform.position, (transform.rotation * Vector3.up).normalized * 0.1f);
        bulletIndex = (bulletIndex + 1) % maxBullets;
	}
}
