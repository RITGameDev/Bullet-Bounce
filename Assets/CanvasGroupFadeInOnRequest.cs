using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupFadeInOnRequest : MonoBehaviour {
	public CanvasGroup Group;
	public float Alpha = 0f;
	public float FadeInAlpha = 1f;
	public float FadeInRate = 0.1f;
	public float Threshold = 0.1f;

	public IEnumerator FadeInCoroutine(){
		while(Alpha <= FadeInAlpha){
			Alpha = Mathf.Lerp (Alpha, FadeInAlpha, FadeInRate);
			if (Mathf.Abs (FadeInAlpha - Alpha) < Threshold)
				Alpha = FadeInAlpha;
			Group.alpha = Alpha;
			yield return new WaitForEndOfFrame();
		}
		yield return 0;	
	}

	public void RequestFadeIn(){
		StartCoroutine (FadeInCoroutine());
	}
}
