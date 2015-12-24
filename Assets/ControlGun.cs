using UnityEngine;
using System.Collections;

public class ControlGun : MonoBehaviour {
	public GameObject bullet;

    int bulletIndex = 0;
    int maxBullets = 50;

    GameObject[] bullets;

    void Start(){
        bullets = new GameObject[maxBullets];
        Vector2 offset = new Vector2(1000, 1000);

        for (int i = 0; i < maxBullets; i++){
            bullets[i] = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
        }
    }

	void Update (){
        transform.rotation = transform.parent.rotation;
        if (Input.GetMouseButtonDown(0))
            Shoot();
    }

	/* Shoots a bullet. */
	void Shoot(){
        bullets[bulletIndex].gameObject.transform.GetComponent<Bullet>().Renew(transform.position, (transform.rotation * Vector3.up).normalized * 0.1f);
        bulletIndex = (bulletIndex + 1) % maxBullets;
        print("argh");
    }
}