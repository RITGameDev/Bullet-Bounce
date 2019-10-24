using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextEqualTo : MonoBehaviour {
	public Text Target;
	public Text ThisText;
	
	// Update is called once per frame
	void Update () {
		ThisText.text = Target.text;
	}
}
