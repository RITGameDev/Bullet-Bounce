using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public KeyCode up, down, left, right;
    public AudioClip lose;
    new public Camera camera;

	float speed = 20f;
    Vector3 screenPos;

    new AudioSource audio;
    Rigidbody2D rb;

    void Awake ()
    {
        audio = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
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
	void Movement(){
        bool verticalMovement = true;
        bool horizontalMovement = true;

        if (Input.GetKey(up))
            rb.velocity = Vector2.up * speed;
        else if (Input.GetKey(down))
            rb.velocity = -Vector2.up * speed;
        else
            verticalMovement = false;

        if (Input.GetKey(left))
            rb.velocity = -Vector2.right * speed;
        else if (Input.GetKey(right))
            rb.velocity = Vector2.right * speed;
        else
            horizontalMovement = false;

        if(!verticalMovement && !horizontalMovement){
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
        }

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
