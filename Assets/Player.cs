using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public KeyCode up, down, left, right;
    public AudioClip lose;
    new public Camera camera;

	float speed = .375f;
    Vector3 screenPos;

    new AudioSource audio;

    void Awake () {
		audio = GetComponent<AudioSource>();
    }

	void Update () {
		Movement();
        Rotation();
	}

    /* Handles rotation by, for now, the cursor position. */
    void Rotation(){
        Vector3 target = Input.mousePosition;
        float hypotenuse = Mathf.Pow(Mathf.Pow(target.x - screenPos.x, 2) + Mathf.Pow(target.y - screenPos.y, 2), 0.5f);

        //JUST REALIZED AFTER MAKING THE BELOW CODE: On a unit circle in Unity, 0degrees would be at point (0, 1), and 270degrees at (1, 0);
        if (target.y < screenPos.y && target.x > screenPos.x)
            transform.rotation = Quaternion.Euler(0, 0, 270 - Mathf.Rad2Deg * Mathf.Acos((target.x - screenPos.x) / hypotenuse));
        else if (target.y < screenPos.y && target.x < screenPos.x)
            transform.rotation = Quaternion.Euler(0, 0, 90 + Mathf.Rad2Deg * Mathf.Atan((target.y - screenPos.y)/(target.x - screenPos.x)));
        else if (target.y > screenPos.y && target.x > screenPos.x)
            transform.rotation = Quaternion.Euler(0, 0, 270 + Mathf.Rad2Deg * Mathf.Asin((target.y - screenPos.y) / hypotenuse));
        else
            transform.rotation = Quaternion.Euler(0, 0, 90 - Mathf.Rad2Deg * Mathf.Asin((target.y - screenPos.y) / hypotenuse));
    }
	/* Handles directional user input by checking the keycodes. Also handles bounds. */
	void Movement() {
		if (Input.GetKey(up) && transform.position.y < 12)
			transform.Translate(Vector2.up * speed, Space.World);
		else if (Input.GetKey(down) && transform.position.y > -12)
			transform.Translate(-Vector2.up * speed, Space.World);
		if (Input.GetKey(left) && transform.position.x > -32)
			transform.Translate(-Vector2.right * speed, Space.World);
		else if (Input.GetKey(right) && transform.position.x < 32)
			transform.Translate(Vector2.right * speed, Space.World);

        screenPos = camera.WorldToScreenPoint(transform.position);
	}

	/* Destroys the player; is called after the sound plays. */
	void Destroy() {
		Destroy(this.gameObject);
	}

	/* Checks for collisions with bullets. */
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Bullet")
			audio.PlayOneShot(lose);
	}
}
