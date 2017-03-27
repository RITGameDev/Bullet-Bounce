using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManager : MonoBehaviour {
	public GameObject LifePrefab;
	public List<GameObject> LifeLocations;
	public List<GameObject> Lives;
	public static int START_LIVES = 5;
	// Use this for initialization
	void Start () {
		Lives = new List<GameObject>();

		// Lives = START_LIVES;
		for (int i = 0; i < START_LIVES; i++) {
			GameObject j = GameObject.Instantiate (LifePrefab);
			j.transform.parent = GameObject.Find ("Canvas").transform;
			j.transform.position = LifeLocations[i].transform.position;
			Lives.Add(j);
		}
	}
	public void Decrease(){
		// Update Lives
		if (Lives.Count > 0) {
			Destroy (Lives [Lives.Count - 1]);
			Lives.RemoveAt (Lives.Count - 1);
		} else
			GameObject.Find ("Gameplay Manager").GetComponent<GameManager> ().EndGame ();
	}
}
