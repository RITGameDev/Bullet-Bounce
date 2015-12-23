using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour {

	public GameObject bullet;
	int poolAmt = 400;
	public List<GameObject> bullets;

	/* Makes and fills a pool of objects. */
	void Awake(){
		bullets = new List<GameObject>();
		for (int i = 0; i < poolAmt; i++){
			GameObject obj;
			obj = Instantiate(bullet);
			obj.SetActive(false);
			bullets.Add(obj); 
		}
	}
}