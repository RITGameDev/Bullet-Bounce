using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public AudioClip cancel;
	public AudioClip bounce;
    Color raw = Color.blue;
    Color pain = Color.red;
    public Color Current;
	public int timeDown =   200;
    float distanceTraveled = 0;
    Vector3 originalPosition;

    new AudioSource audio;
    Rigidbody2D rb;
    Collider2D hitbox;

    /* Gets the components and moves the bullet. */
    void Awake() {
        originalPosition = transform.position;
        raw = GetComponent<SpriteRenderer>().color;

        hitbox = GetComponent<Collider2D>();
		hitbox.enabled = false;
		rb = GetComponent<Rigidbody2D>();
		audio = GetComponent<AudioSource>();
        Hide();
	}
    void Update(){
        distanceTraveled = Mathf.Pow(Mathf.Pow(transform.position.x - originalPosition.x, 2) + Mathf.Pow(transform.position.y - originalPosition.y, 2), 0.5f);
        if (!hitbox.enabled && distanceTraveled > 2)
            hitbox.enabled = true;
        Time.timeScale = 1.2f;
        if(timeDown>0)
            timeDown--;

        GetComponent<SpriteRenderer>().color = Current = Color.Lerp(pain, raw, mapClamped(timeDown, 0, 1500, 0.0f, 1.0f));
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
        rb.AddForce(new Vector2(rotation.x, rotation.y)*1.6f/5f);
        Current = Color.blue;
        timeDown = 100;
    }
	/* Handles walls, players, and other bullets. */
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag.Equals("Bullet")) {
			// Add X points to the player's score
			GameManager.Instance.BulletsHitEachOther();
			rb.velocity = (Vector2.zero);
			audio.PlayOneShot(cancel);
			Invoke("Hide", cancel.length);
		}
        else if (coll.gameObject.tag.Equals("Wall"))
        {
            audio.PlayOneShot(bounce);
		}else if (coll.gameObject.tag.Equals("Player")&&timeDown<=0) {
            Hide();
        }
    }

	public bool IsReady(){
        bool b = timeDown <= 0;
        return b;
	}

    public float getDistanceTraveled(){
        return distanceTraveled;
    }
	
    public float mapClamped(float r, float a, float b, float c, float d)
    {
        return ((r - a) / (b - a)) * (d - c) + c;
    }
}
