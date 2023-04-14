using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainManager : MonoBehaviour
{
	public AudioSource audioSource;
	public AudioLowPassFilter lowFilter;

	public float duration = 0.5f;
	public float startCutoff = 22000f;
	public float endCutoff = 5000f;
	public float startVolume = 0.6f;
	public float endVolume = 0.18f;

	private void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
			StartCoroutine(StartFade(audioSource, lowFilter, duration, endCutoff, endVolume));
		}
	}

	private void OnTriggerExit(Collider col) {
		if (col.tag == "Player") {
			StartCoroutine(EndFade(audioSource, lowFilter, duration, startCutoff, startVolume));
		}
	}

	public static IEnumerator StartFade(AudioSource audioSource, AudioLowPassFilter lowFilter, float duration, float targetCutoff, float targetVolume)
    {
        float currentTime = 0;
        float startCutoff = lowFilter.cutoffFrequency;
		float startVolume = audioSource.volume;
        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            lowFilter.cutoffFrequency = Mathf.Lerp(startCutoff, targetCutoff, currentTime / duration);
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }

	public static IEnumerator EndFade(AudioSource audioSource, AudioLowPassFilter lowFilter, float duration, float targetCutoff, float targetVolume)
    {
        float currentTime = 0;
        float startCutoff = lowFilter.cutoffFrequency;
		float startVolume = audioSource.volume;
        while (currentTime < duration) {
            currentTime += Time.deltaTime;
            lowFilter.cutoffFrequency = Mathf.Lerp(startCutoff, targetCutoff, currentTime / duration);
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}
