using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    private CanvasGroup canvasGroup;
	private float fadeSpeed;
	public float FadeSpeed;
	public bool CanFade;
	private void Start() {
		canvasGroup = GetComponent<CanvasGroup>();
	}

	void Update () {
		if (CanFade) {
			if (canvasGroup.alpha == 0) {
				if (FadeSpeed == 0)
					fadeSpeed = 0.03f;
				else
					fadeSpeed = FadeSpeed;
			}
			else if (canvasGroup.alpha == 1) {
				if (FadeSpeed == 0)
					fadeSpeed = -0.03f;
				else
					fadeSpeed = -FadeSpeed;
			}
		}
		else {
			fadeSpeed = 0;
		}
		
		canvasGroup.alpha += fadeSpeed;
		
		if (CanFade && (canvasGroup.alpha == 0 || canvasGroup.alpha == 1)) {
			CanFade = false;
		}
	}
}
