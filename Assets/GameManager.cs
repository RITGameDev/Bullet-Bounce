using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static GameManager Instance;
	public bool IsPlaying = true;
	public GameObject Score;
	private UnityEngine.UI.Text ScoreComponent;
	public float Delay = 0;
	private float time = 10;
	void Start(){
		ScoreComponent = Score.GetComponent<UnityEngine.UI.Text> ();
		Instance = this;
	}

	// Update is called once per frame
	void Update () {
		switch (IsPlaying) {
		case true:
			time--;
			if (time <= 0) {
				updateScore ();
				time += Delay;
			}
			break;
		case false:
			break;
		}
	}

	public void EndGame(){
		IsPlaying = false;
		Destroy(GameObject.Find ("P1"));
	}

	private void updateScore(){
		int score = System.Convert.ToInt32 (ScoreComponent.text);
		Bullet[] bulletsInScene = GameObject.FindObjectsOfType<Bullet>();
		score += bulletsInScene.Length;
		ScoreComponent.text = "" + score;
	}
	public void BulletsHitEachOther(){
		int score = System.Convert.ToInt32 (ScoreComponent.text);
		Bullet[] bulletsInScene = GameObject.FindObjectsOfType<Bullet>();
		score += bulletsInScene.Length*bulletsInScene.Length;
		ScoreComponent.text = "" + score;
	}
}
