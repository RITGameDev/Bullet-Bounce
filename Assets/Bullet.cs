using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public AudioClip cancel;
	public AudioClip bounce;

    float distanceTraveled = 0;
    Vector3 originalPosition;

    new AudioSource audio;
    Rigidbody2D rb;
    Collider2D hitbox;

    /* Gets the components and moves the bullet. */
    void Awake() {
        originalPosition = transform.position;
		hitbox = GetComponent<Collider2D>();
		hitbox.enabled = false;
		rb = GetComponent<Rigidbody2D>();
		audio = GetComponent<AudioSource>();
        Hide();
	}
    void Update(){
        distanceTraveled = Mathf.Pow(Mathf.Pow(transform.position.x - originalPosition.x, 2) + Mathf.Pow(transform.position.y - originalPosition.y, 2), 0.5f);
        if (!hitbox.enabled && distanceTraveled > 1)
            hitbox.enabled = true;
    }
	/* Destroys the bullet; is called after the sound plays. */
	void Hide() {
        transform.position = originalPosition = new Vector3(1000, 1000, 0);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;
        hitbox.enabled = false;
	}
    public void Renew(Vector3 position, Vector3 rotation){
        Hide();
        transform.position = originalPosition = position;
        transform.eulerAngles = rotation; // Rotation may not matter for the bullet.... unsure tho so I'll leave it :P
        rb.AddForce(new Vector2(rotation.x, rotation.y));
    }
	/* Handles walls, players, and other bullets. */
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag.Equals("Bullet")) {
			// Add X points to the player's score
			rb.velocity = (Vector2.zero);
			audio.PlayOneShot(cancel);
			Invoke("Hide", cancel.length);
		}
        else if (coll.gameObject.tag.Equals("Wall"))
        {
            audio.PlayOneShot(bounce);
        }else if (coll.gameObject.tag.Equals("Player")) {
            Hide();
        }
    }

    public float getDistanceTraveled(){
        return distanceTraveled;
    }
		
}
