using System.Collections;
using UnityEngine;

namespace RPG.SceneManagement
{
	public class Fader : MonoBehaviour
	{
		CanvasGroup canvasGroup;

		private void Start()
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}
		public IEnumerator FadeOut(float time)
		{
			while (canvasGroup.alpha < 1f)
			{
				float deltaAlpha = Time.deltaTime / time;
				canvasGroup.alpha += deltaAlpha;
				yield return null;
			}
		}

		public IEnumerator FadeIn(float time)
		{
			while (canvasGroup.alpha > 0f)
			{
				float deltaAlpha = Time.deltaTime / time;
				canvasGroup.alpha -= deltaAlpha;
				yield return null;
			}
		}

		IEnumerator FadeOutIn(float time)
		{
			yield return FadeOut(1f);
			yield return FadeIn(1f);
		}
	}
}
