using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour {
	public CanvasGroupFadeInOnRequest CanvasManager;
	public Image Bg;
	public float Alpha = 0f;
	public float FadeInAlpha = 216f;
	public float FadeInRate = 0.1f;
	public float Threshold = 1f;
	public float DelayTime = 5f;
    public bool DO_IT = false;
    private void Update()
    {
        if (DO_IT)
            FadeIn();
    }
    // Use this for initialization
    public void FadeIn() {
		CanvasManager.RequestFadeIn ();
		StartCoroutine (FadeInCoroutine());
	}

	public IEnumerator FadeInCoroutine()
    {
        StartCoroutine(DelayToStart());
        Color col;
		Bg = GetComponent<Image> ();
		while(Alpha <= FadeInAlpha){
			Alpha = Mathf.Lerp (Alpha, FadeInAlpha, FadeInRate);
			if (Mathf.Abs (FadeInAlpha - Alpha) < Threshold)
				Alpha = FadeInAlpha;
			col = Bg.color;
			col.a = Alpha;
			Bg.color = col;
			yield return new WaitForEndOfFrame();
		}
		yield return 0;	
	}

	public IEnumerator DelayToStart(){
		yield return new WaitForSeconds(DelayTime);
		SceneManager.LoadScene ("StartMenu");
		yield return 0;
	}
}
