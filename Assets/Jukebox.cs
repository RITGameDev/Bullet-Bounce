using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Jukebox : MonoBehaviour {

	public List<AudioClip> playlist;
	AudioSource audio;

	void Awake() {
		audio = GetComponent<AudioSource>();
		PickSong();
	}

	/* Picks a song in the jukebox randomly. */
	void PickSong () {
		int songIndex = Random.Range(0, playlist.Count);
		AudioClip currSong = playlist[songIndex];
		audio.PlayOneShot(currSong);
		Invoke("PickSong", currSong.length);
	}
}
